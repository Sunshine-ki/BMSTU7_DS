using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace lab_05
{
    public class Signer
    {
        #region params
        public string PathToPublicKey { get; set; } = @"C:\Users\ASukocheva\source\repos\BMSTU7_DS\lab_05\lab_05\lab_05\keys\public_key.txt";
        public string PathToPrivateKey { get; set; } = @"C:\Users\ASukocheva\source\repos\BMSTU7_DS\lab_05\lab_05\lab_05\keys\private_key.txt";
        public string PathToData { get; set; } = @"C:\Users\ASukocheva\source\repos\BMSTU7_DS\lab_05\lab_05\lab_05\data\test.txt";
        public string PathToSigned { get; set; } = @"C:\Users\ASukocheva\source\repos\BMSTU7_DS\lab_05\lab_05\lab_05\data\signed";
        public string HashName { get; set; } = "SHA256";
        #endregion

        public void GenerateKeys()
        {
            using var rsa = new RSACryptoServiceProvider();

            var publikKey = rsa.ToXmlString(false); // false - запишет только публичный ключ.
            var privateKey = rsa.ToXmlString(true); // true - запшет и приватный и публичнфй ключ.

            File.WriteAllText(PathToPublicKey, publikKey);
            File.WriteAllText(PathToPrivateKey, privateKey);
        }

        public byte[] CreateSigned()
        {
            var hashValue = getHashFromFile(PathToData);
            using var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(getPrivateKey());

            var rsaFormatter = new RSAPKCS1SignatureFormatter(rsa);
            rsaFormatter.SetHashAlgorithm(HashName);

            var signedHash = rsaFormatter.CreateSignature(hashValue);

            return signedHash;
        }

        public bool CheckSigned()
        {
            using var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(getPublicKey());

            var signed = readDataFromFile(PathToSigned);
            var fileHash = getHashFromFile(PathToData);

            var rsaDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
            rsaDeformatter.SetHashAlgorithm(HashName);

            var result = rsaDeformatter.VerifySignature(fileHash, signed);
            
            return result;
        }
        private string getPrivateKey()
        {
            var key = File.ReadAllText(PathToPrivateKey);
            return key;
        }

        private string getPublicKey()
        {
            var key = File.ReadAllText(PathToPublicKey);
            return key;
        }

        private byte[] getHash(byte[] fileData)
        {
            var hashAlgorithm = HashAlgorithm.Create(HashName);

            var hashValue = hashAlgorithm.ComputeHash(fileData);

            return hashValue;
        }

        private byte[] getHashFromFile(string path)
        {
            var fileData = readDataFromFile(path);
            var hashValue = getHash(fileData);
            return hashValue;
        }

        private byte[] readDataFromFile(string path)
        {
            using var fs = File.OpenRead(path);

            var fileData = new byte[fs.Length];
            fs.Read(fileData, 0, (int)fs.Length);

            return fileData;
        }
    }
}
