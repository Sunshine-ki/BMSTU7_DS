using System;
using System.IO;

namespace lab_05
{
    class Program
    {
        static void Run()
        {
            var signer = new Signer();

            Console.Write(Constants.Instruction);
            var answer = Convert.ToInt32(Console.ReadLine());

            switch(answer)
            {
                case 1:
                    signer.GenerateKeys();
                    break;

                case 2:
                    var signed = signer.CreateSigned();
                    File.WriteAllBytes(signer.PathToSigned, signed);
                    break;

                case 3:
                    if (signer.CheckSigned())
                    {
                        Console.WriteLine("The signature was verified");
                    }
                    else
                    {
                        Console.WriteLine("The signature does not match");
                    }

                    break;
            }
        }

        static void Main(string[] args)
        {
            Run();
        }
    }
}
