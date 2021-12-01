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
            Console.WriteLine("1 - compress; 2 - decompress");
            var answer = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Хотите получить доп. информацию? 0 - нет; 1 - да\n");
            var info = Convert.ToBoolean(Convert.ToInt32(Console.ReadLine()));


            if (answer == 1)
            {
                // TODO: ReadFileIntoByteArray??? and not problem
                var dataBit = Reader.ReadFileIntoBoolArray(Constants.PathToData);
                var dataByte = Converter.ConvertListBoolToListByte(dataBit);
                // TODO: param for output all substitutions
                var compressedData = Huffman.Compress(dataByte, out int min, out int max);

                Console.Write("\n");
                Console.WriteLine($"Прочитанное кол-во бит = {dataBit.Count} байт = {dataByte.Count}");
                Console.WriteLine($"Минимальная длина заменяемого кода (Min) = {min}");
                Console.WriteLine($"Максимальная длина заменяемого кода (Max) = {max}");
                Console.WriteLine($"Сжатые данные. Кол-во бит = {compressedData.Count} байт = {compressedData.Count / 8}");
            }
            else if (answer == 2)
            {
                var compressedData = Reader.ReadFileIntoBoolArray(Constants.PathToCompressedData);

                var readMeta = Reader.ReadFileIntoBoolArray(Constants.PathToMeta);
                int dataSize = 0;
                var readSubstitutions = Metadata.GetSustitutionsFromBitList(readMeta, ref dataSize);

                Console.WriteLine($"Считанные метаданные кол-во = {readSubstitutions.Count} бит {readSubstitutions.Count / 8} байт");
                if (info) Huffman.PrintSubstitutions(readSubstitutions);

                var decompressedData = Huffman.Decompress(readSubstitutions, compressedData.Take(dataSize).ToList());
                Writer.WriteByteArrayToFile(decompressedData, Constants.PathToDecompressedData);
            }
        }

        static void Main(string[] args)
        {
            Run();
        }
    }
}
