using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace des
{
    public static class Key
    {
        public static List<bool> ConvertStringToBoolList(string key)
        {
            var result = new List<bool>();

            foreach (char symbol in key)
            {
                // Переводит символ в массив битов 
                // PadLeft - добавляет нули слева.
                var current = Convert.ToString((byte)symbol, 2).PadLeft(8, '0').Select(bit => bit == '1').ToList();
                result.AddRange(current);
            }
            

            return result;
        }

        public static List<List<bool>> Generate(List<bool> key)
        {
            List<List<bool>> result = new List<List<bool>>();

            var ci  = DataOperations.Permutate(key, Constants.C0);
            var di = DataOperations.Permutate(key, Constants.D0);

            for (int i = 0; i < Constants.Si.Count; i++)
            {
                var cd = ci.Concat(di).ToList();
                result.Add(DataOperations.Permutate(cd, Constants.CP));
                
                ci = LeftShift(ci, Constants.Si[i]);
                di = LeftShift(di, Constants.Si[i]);
            }

            // Результат 16  ключей по 48 бит.
            return result;
        }
        
        public static List<bool> LeftShift(List<bool> arr, int countOfShift)
        {
            if (countOfShift > arr.Count)
            {
                return arr;
            }

            List<bool> result = arr.Skip(countOfShift).ToList();
            result.AddRange(arr.Take(countOfShift));
            
            return result;
        }


        //public static void AddBytes(List<bool> key)
        //{
        //    List<bool> result = new List<bool>();
        //    int numberOfOnes = 0;
        //    int currentOldIndex = 0, currentNewIndex = 0;

        //    for (int i = 0, j; i < 8; i++)
        //    {
        //        for (j = 0; j < 7; j++)
        //        {
        //            //Console.WriteLine($"currentNewIndex = {currentNewIndex}");
        //            //Console.WriteLine($"currentOldIndex = {currentOldIndex}");
        //            result.Add(key[currentOldIndex]);
        //            currentOldIndex++;
        //            currentNewIndex++;
        //        }
        //        result.Add(false);

        //        currentNewIndex++;
        //    }

        //    Console.WriteLine(result.Count);
        //    for (int i = 0; i < result.Count; i++)
        //    {
        //        if ((i % 9) == 0)
        //        {
        //            Console.WriteLine();
        //        }
        //        Console.Write($"{result[i]} ");
        //    }

        //for (int i = key.Count - 1; i > 0; i -= 8)
        //{
        //    Console.WriteLine($"aAa {i}");
        //    key.Insert(i, false);
        //}

        //Console.WriteLine(key.Count);
        //for (int i = 0; i < key.Count; i++)
        //{
        //    if ((i % 9) == 0)
        //    {
        //        Console.WriteLine();
        //    }
        //    Console.Write($"{key[i]} ");
        //}


        //Console.WriteLine("AAAAAAAAa");
        //Console.WriteLine(key.Count);
        //for (int i = 0; i < key.Count; i++)
        //{
        //    if ((i % 9) == 0)
        //    {
        //        Console.WriteLine();
        //    }
        //    Console.Write($"{key[i]} ");
        //}

        //for (int i = 0, j; i < 8; i++)
        //{
        //    for (j = 0; j < 8; j++)
        //    {
        //        Console.WriteLine($"i = {i} j = {j} currentIndex = {currentIndex} len(key) = {key.Count}");
        //        if (key[currentIndex])
        //        {
        //            numberOfOnes++;
        //        }
        //        Console.Write($"{key[currentIndex]} ");
        //        currentIndex++;
        //    }
        //    key[currentIndex] = !Convert.ToBoolean(numberOfOnes % 2);
        //    numberOfOnes = 0;
        //}
        //}
    }
}
