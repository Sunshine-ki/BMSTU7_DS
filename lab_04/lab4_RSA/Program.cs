using System;
using System.IO;

namespace lab_04
{
    class Program
    {
        static void Main(string[] args)
        {
            RSA rsaMachine = new RSA();

            FileOperations.ProcessFileToBinary(Constants.FilenameIN, Constants.FilenameEncr, rsaMachine.Encrypt);
            FileOperations.ProcessBinaryToNormal(Constants.FilenameEncr, Constants.FilenameDecr, rsaMachine.Decrypt);

            Console.WriteLine("Ok");
            Console.ReadLine();
        }
    }
}