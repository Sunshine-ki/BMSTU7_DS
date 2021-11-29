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
            var huffman = new Huffman();

            var dataBit = Reader.ReadFileToBoolArray(Constants.PathToData);
            var dataByte = Converter.ConvertListBoolToListByte(dataBit);
            var weights = huffman.CreateWeights(dataByte);

            foreach (var elem in weights.OrderBy(w => w.Value))
            {
                Console.WriteLine(string.Format("{0, 6} = {1,6}", elem.Key, elem.Value));
            }

            var tree = huffman.CreateTree(weights);

            var substitutions = huffman.CreateSubstitutions(tree);

            Console.Write("\n");
            var min = substitutions.ElementAt(0).Value.Count;
            var max = substitutions.ElementAt(0).Value.Count;
            
            Console.WriteLine($"Изначальные метаданные кол-во = {substitutions.Count}");
            foreach (var elem in substitutions.OrderBy(e => e.Value.Count))
            {
                Console.Write($"{elem.Key} ");
                
                if (elem.Value.Count < min) min = elem.Value.Count;
                if (elem.Value.Count > max) max = elem.Value.Count;

                foreach (var b in elem.Value)
                {
                    Console.Write(b ? 1 : 0);
                }
                Console.Write("\n");
            }
            Console.WriteLine("__________");

            Console.WriteLine($"\n Min = {min} Max = {max} res.count = {substitutions.Count}");

            var compressedData = huffman.Replace(dataByte, substitutions);
            Writer.WriteBoolArrayToFile(compressedData, Constants.PathToNewData);

            Console.WriteLine($"dataBit.Count = {dataBit.Count} (begin size)");
            Console.WriteLine($"GetMetaSize for substitutions = {huffman.GetMetaSize(substitutions)}");


            var metaArrBit = huffman.CreateMeta(substitutions);
            Writer.WriteBoolArrayToFile(metaArrBit, Constants.PathToNewMeta);

            var readMeta = Reader.ReadFileToBoolArray(Constants.PathToNewMeta);
            var readSubstitutions = huffman.ConvertListBitToSubstitutions(readMeta);

            Console.WriteLine($"Считанные метаданные кол-во = {readSubstitutions.Count}");
            foreach (var elem in readSubstitutions.OrderBy(e => e.Value.Count))
            {
                Console.Write($"{elem.Key} ");

                if (elem.Value.Count < min) min = elem.Value.Count;
                if (elem.Value.Count > max) max = elem.Value.Count;

                foreach (var b in elem.Value)
                {
                    Console.Write(b ? 1 : 0);
                }
                Console.Write("\n");
            }
            Console.WriteLine("__________");

            var decompressedData = huffman.Decompress(readSubstitutions, compressedData);
            Writer.WriteByteArrayToFile(decompressedData, Constants.PathToNewDecompressData);


            //Writer.WriteBoolArrayToFile(dataBit, Constants.PathToNewData);
        }

        static void Main(string[] args)
        {
            Run();
        }


    }
}
