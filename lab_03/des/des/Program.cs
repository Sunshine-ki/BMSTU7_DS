using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace des
{
    class Program
    {
        static readonly string root = @"C:\Users\ASukocheva\source\repos\BMSTU7_DS\lab_03\des\des\data\text\";

        static readonly string path = Path.Combine(root, "simple.txt");
        static readonly string pathToEncryptedFile = Path.Combine(root, "secret.txt"); 
        static readonly string pathToDecryptedFile = Path.Combine(root, "simpleBegin.txt"); 

        // Img
        //static readonly string path = Path.Combine(root, "cat.jpg");
        //static readonly string pathToEncryptedFile = Path.Combine(root, "encryptCat.jpg"); 
        //static readonly string pathToDecryptedFile = Path.Combine(root, "DecryptCat.jpg");

        static void Process()
        {
            Console.WriteLine("Input key:");
            string keyString = Console.ReadLine();//= "12345678";//"abcdifgh";

            if (keyString.Length != 8)
            {
                Console.WriteLine("Длина ключа должна быть 8 символов");
                return;
            }

            var key = Key.ConvertStringToBoolList(keyString);
            var keys = Key.Generate(key);

            int addCount = 0;
            var blocks = Reader.Read(path, ref addCount);

            List<List<bool>> encryptedBlocks = new List<List<bool>>();

            foreach (var block in blocks)
            {
                encryptedBlocks.Add(DataOperations.Encrypt(block, keys));
            }

            Writer.Write(encryptedBlocks, pathToEncryptedFile);

            int addCount2 = 0;
            var encryptedBlocksFromFile = Reader.Read(pathToEncryptedFile, ref addCount2);
            var beginData = new List<List<bool>>();

            foreach (var block in encryptedBlocksFromFile)
            {
                beginData.Add(DataOperations.Decrypt(block, keys));
            }
            Writer.Write(beginData, pathToDecryptedFile, addCount);

        }
        static void Main(string[] args)
        {
            Process();
        }
    }
}

//static void Process()
//{
//    Console.WriteLine("Input key:");
//    string keyString = Console.ReadLine();// "12345678";//"abcdifgh";

//    if (keyString.Length != 8)
//    {
//        Console.WriteLine("Длина ключа должна быть 8 символов");
//        return;
//    }

//    var key = Key.ConvertStringToBoolList(keyString);
//    var keys = Key.Generate(key);

//    Console.WriteLine("e - encrypt / d - decrypt");
//    char mode = Convert.ToChar(Console.ReadLine());

//    int addCount = 0; // Сколько дописали в конец нулей.
//    List<List<bool>> blocks;
//    switch (mode)
//    {
//        case 'e':
//            blocks = Reader.Read(path, ref addCount);

//            List<List<bool>> encryptedBlocks = new List<List<bool>>();

//            foreach (var block in blocks)
//            {
//                encryptedBlocks.Add(DataOperations.Encrypt(block, keys));
//            }

//            Writer.Write(encryptedBlocks, pathToEncryptedText, addCount);
//            Console.WriteLine($"Count = {addCount}");
//            break;
//        case 'd':
//            Console.Write("Count = ");
//            addCount = Convert.ToInt32(Console.ReadLine());
//            Console.WriteLine(addCount);
//            blocks = Reader.Read(pathToEncryptedText, ref addCount);

//            List<List<bool>> beginData = new List<List<bool>>();
//            foreach (var block in blocks)
//            {
//                beginData.Add(DataOperations.Decrypt(block, keys));
//            }
//            Writer.Write(beginData, pathToDecryptedText, addCount);
//            break;
//    }
//}