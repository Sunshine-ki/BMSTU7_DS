using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace lab_06
{
    public static class Reader
    {
        public static List<bool> ReadFileIntoBoolArray(string path)
        {
            var result = new List<bool>();
            int elem;

            using var stream = File.OpenRead(path);
            while ((elem = stream.ReadByte()) > -1)
            {
                byte b = (byte)elem; // byte
                var bitArray = Converter.ConvertIntToBitArray(b, 8);
                //Convert.ToString(b, 2).PadLeft(8, '0').Select(bit => bit == '1').ToList();
                result.AddRange(bitArray);
            }

            return result;
        }
    }
}
