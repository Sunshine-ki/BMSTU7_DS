using System;
using System.Collections.Generic;
using System.Linq;

namespace Enigma
{
    class Program
    {
        static void Main(string[] args)
        {
            Process();
        }

        static private void Process()
        {
            bool flag = true;
            Machine machine = new Machine();
            
            while (flag)
            {
                Console.WriteLine("\n0 - Exit\n1 - Encode\n2 - Set position\n3 - Get position\n4 - View alphabet");
                Console.Write("Answer: ");
                int answer = Convert.ToInt32(Console.ReadLine());

                switch (answer)
                {
                    case 0:
                        flag = false;
                        break;
                    case 1:
                        Console.WriteLine("Input codes:");
                        var userString = Console.ReadLine().Select(c => Constants.alphabet.FindIndex(e => e == Convert.ToChar(c))).ToList();
                        foreach (var symbol in userString)
                        {
                            int code = machine.Encode(symbol);
                            Console.Write($"{Constants.alphabet[code]}");
                        }
                        Console.WriteLine();
                        break;
                    case 2:
                        Console.WriteLine($"\nInput position (left roter, middle roter, right roter) Max = {Constants.MAX_INDEX_VALUE}");
                        List<int> inputPosition = Console.ReadLine().Split(" ").Select(Int32.Parse).ToList();
                        machine.SetPosition(inputPosition[0], inputPosition[1], inputPosition[2]);
                        Console.WriteLine();
                        break;
                    case 3:
                        List<int> position = machine.GetPosition();
                        Console.WriteLine($"\nPosition = {position[0]}, {position[1]}, {position[2]}\n");
                        break;
                    case 4:
                        Constants.alphabet.ForEach(c => Console.Write($"\"{c}\" "));
                        break;

                }
            }
        }
    }
}
