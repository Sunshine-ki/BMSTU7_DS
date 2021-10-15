using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace des
{
    public static class Constants
    {
        private static readonly string _directory = @"C:\Users\ASukocheva\source\repos\BMSTU7_DS\lab_03\des\des\data\";

        // For key
        public static List<int> C0;
        public static List<int> D0;
        public static List<int> CP;
        public static List<int> Si;

        // For data
        public static List<int> IP;
        public static List<int> E;
        public static List<List<int>> Sblocks;
        public static List<int> P;
        public static List<int> IPInverse;
        
        static Constants()
        {
            C0 = ResuceByOne(ReadFromFile(Path.Combine(_directory, "Key", "C0.txt")));
            D0 = ResuceByOne(ReadFromFile(Path.Combine(_directory, "Key", "D0.txt")));
            CP = ResuceByOne(ReadFromFile(Path.Combine(_directory, "Key", "CP.txt")));
            // Si - это сдвиги, там нет индексов, поэтому уменьшать на единицу не нужно. 
            Si = ReadFromFile(Path.Combine(_directory, "Key", "Si.txt"));

            IP = ResuceByOne(ReadFromFile(Path.Combine(_directory, "IP.txt")));
            E = ResuceByOne(ReadFromFile(Path.Combine(_directory, "E.txt")));
            P = ResuceByOne(ReadFromFile(Path.Combine(_directory, "P.txt")));
            IPInverse = ResuceByOne(ReadFromFile(Path.Combine(_directory, "IPInverse.txt")));

            Sblocks = new List<List<int>>();
            // S-blocks тоже не нужно уменьшать на единицу, т.к. там значения от 0 до 15.
            for (int i = 0; i < 8; i++)
            {
                Sblocks.Add(ReadFromFile(Path.Combine(_directory, "S-blocks", $"{i + 1}.txt")));
                // Console.WriteLine(Sblocks[i].Count);
            }
        }

        private static List<int> ReadFromFile(string path)
        {
            List<int> result = File.ReadAllText(path)
                                   .Split(" ")
                                   .Select(elem => Convert.ToInt32(elem))
                                   .ToList();

            return result;
        }

        // Потому что в файле индексация с единицы! (а мы же пишем не на паскале) 
        private static List<int> ResuceByOne(List<int> arr)
        {
            return arr.Select(elem => elem - 1).ToList();
        }

    }
}
