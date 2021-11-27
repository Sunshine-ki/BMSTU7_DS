using System;
using System.IO;
using System.Security.Cryptography;
using System.Linq;

namespace lab_05
{
    class Program
    {
        static string pathToPublicKey = @"C:\Users\ASukocheva\source\repos\BMSTU7_DS\lab_05\lab_05\lab_05\keys\public_key.txt";
        static string pathToPrivateKey = @"C:\Users\ASukocheva\source\repos\BMSTU7_DS\lab_05\lab_05\lab_05\keys\private_key.txt";
        static string pathToData = @"C:\Users\ASukocheva\source\repos\BMSTU7_DS\lab_05\lab_05\lab_05\data\Redhead_Cat.jpg";
        static string pathToSigned = @"C:\Users\ASukocheva\source\repos\BMSTU7_DS\lab_05\lab_05\lab_05\data\signed";

        private static byte[] readDataFromFile(string path)
        {
            using var fs = File.OpenRead(path);

            var fileData = new byte[fs.Length];
            fs.Read(fileData, 0, (int)fs.Length);
            
            return fileData;
        }

        private static void writeDataToFile(string path, byte[] data)
        {
            File.WriteAllBytes(path, data);
        }


        private static byte[] getHash(byte[] fileData)
        {
            using var sha256 = SHA256.Create();
            var hashValue = sha256.ComputeHash(fileData);
            
            return hashValue;
        }

        private static byte[] getHashFromFile(string path)
        {
            var fileData = readDataFromFile(path);
            var hashValue = getHash(fileData);
            return hashValue;
        }

        private static void GenerateKeys()
        {
            using RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

            var privateParams = rsa.ExportParameters(true);
            var publicParams = rsa.ExportParameters(false);

            var publikKey = rsa.ToXmlString(false); // false - запишет только публичный ключ.
            var privateKey = rsa.ToXmlString(true); // true - запшет и приватный и публичнфй ключ.

            File.WriteAllText(pathToPublicKey, publikKey);
            File.WriteAllText(pathToPrivateKey, privateKey);
        }

        private static string GetPrivateKey()
        {
            var key = File.ReadAllText(pathToPrivateKey);
            return key;
        }

        private static string GetPublicKey()
        {
            var key = File.ReadAllText(pathToPublicKey);
            return key;
        }

        private static byte[] CreateSigned()
        {
            var hashValue = getHashFromFile(pathToData);
            using RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

            var privateKey = GetPrivateKey();
            rsa.FromXmlString(privateKey);
            var data = rsa.Encrypt(hashValue, false); // Второй параметр что-то для windows xp, нас не интересует....
            
            return data;
        }

        private static bool CheckSigned()
        {
            using RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            var publicKey = GetPublicKey();
            rsa.FromXmlString(publicKey);

            var signed = readDataFromFile(pathToSigned);
            var signedHash = rsa.Decrypt(signed, false);
            var fileHash = getHashFromFile(pathToData);

            return signedHash.SequenceEqual(fileHash);
        }


        static void Main(string[] args)
        {
            //GenerateKeys();

            // Создание подписи
            //var signed = CreateSigned();
            //writeDataToFile(pathToSigned, signed);

            Console.WriteLine(CheckSigned());


        }
    }
}
