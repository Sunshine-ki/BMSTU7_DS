using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace lab_06
{
    class Program
    {
        static void Run()
        {
            //var huffman = new Huffman();

            //var weights = huffman.CreateWeights(Constants.PathToData);

            //foreach(var elem in weights)
            //{
            //    Console.Write($"{elem.Key} = {elem.Value}\t");
            //}

            //var min = huffman.GetMinAndDelete(weights);
            //Console.WriteLine($"\nmin by value = {min.Key} {min.Value}");
        }

        static void Main(string[] args)
        {
            //Run();
            var arr = Reader.ReadFileToBoolArray(Constants.PathToData);
            Writer.WriteBoolArrayToFile(arr, Constants.PathToNewData);
        }
    }
}
