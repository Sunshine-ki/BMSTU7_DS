using System;
using System.IO;

namespace lab_04
{
    class Program
    {
        static void Main(string[] args)
        {
            //RSA rsaMachine = new RSA();

            Console.WriteLine($"AAA {12 % 32}");
            int a = 12;
            int b = 32;
            Console.WriteLine($"GreatestCommonDivisor({a}, {b}) = {MyMath.GreatestCommonDivisor(a,b)}");

            //FileOperations.ProcessFileToBinary(Constants.FilenameIN, Constants.FilenameEncr, rsaMachine.Encrypt);
            //FileOperations.ProcessBinaryToNormal(Constants.FilenameEncr, Constants.FilenameDecr, rsaMachine.Decrypt);

            Console.WriteLine("Ok");
            Console.ReadLine();
        }
    }
}