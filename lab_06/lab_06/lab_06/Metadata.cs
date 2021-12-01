using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lab_06
{
    public static class Metadata
    {
        public static List<bool> Create(Dictionary<byte, List<bool>> substitutions, int dataSize)
        {
            var result = new List<bool>();

            var metaSize = GetMetaSize(substitutions);
            result.AddRange(Converter.ConvertIntToBitArray(metaSize, 32)); // 32 because int is 32 bit
            result.AddRange(Converter.ConvertIntToBitArray(dataSize, 32)); 
            
            substitutions.ForEach(sub =>
            {
                result.AddRange(Converter.ConvertIntToBitArray(sub.Key, 8)); // Symbol
                result.AddRange(Converter.ConvertIntToBitArray(sub.Value.Count, 8)); // Lenght code
                result.AddRange(sub.Value); // code
            });

            while (result.Count % 8 != 0) result.Add(false);

            return result;
        }

        // Return size in bit
        public static int GetMetaSize(Dictionary<byte, List<bool>> substitutions)
        {
            int size = 0; // 32 bit

            // Длина каждого заменяемого кода.
            substitutions.ForEach(sub => { size += sub.Value.Count; });

            // 2 байта под каждую замену.
            // 1ый байт - [0-255] какой символ
            // 2ой байт - [0-255] размер заменяемого кода
            size += 2 * substitutions.Count * 8;

            return size;
        }

        public static Dictionary<byte, List<bool>> GetSustitutionsFromBitList(List<bool> data, ref int dataSize)
        {
            var result = new Dictionary<byte, List<bool>>();
            List<bool> currentCode = new List<bool>();

            int metaSize = Converter.ConvertListBoolToInt(data.Take(32).ToList());
            Console.WriteLine("begin dataSize");
            dataSize = Converter.ConvertListBoolToInt(data.Skip(32).Take(32).ToList());
            //Console.WriteLine($"[GetSustitutionsFromBitList] metaSize = {metaSize}");
            Console.WriteLine("end dataSize");

            var realMetaData = data.Skip(64).Take(metaSize).ToList();

            for (int i = 0; i < realMetaData.Count;)
            {
                var symbol = Converter.ConvertListBoolToInt(realMetaData.Skip(i).Take(8).ToList());
                i += 8;

                var codeSize = Converter.ConvertListBoolToInt(realMetaData.Skip(i).Take(8).ToList());
                i += 8;

                for (int j = 0; j < codeSize; j++)
                {
                    currentCode.Add(realMetaData[i]);
                    i++;
                }

                result.Add((byte)symbol, currentCode.GetRange(0, currentCode.Count));
                currentCode.Clear();
            }

            return result;
        }

    }
}
