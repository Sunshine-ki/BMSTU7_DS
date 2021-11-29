using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace lab_06
{
    public static class Reader
    {
        public static List<bool> ReadFileToBoolArray(string path)
        {
            var result = new List<bool>();
            int elem;

            using var stream = File.OpenRead(path);
            while ((elem = stream.ReadByte()) > -1)
            {
                byte b = (byte)elem;
                var tmp = Convert.ToString(b, 2).PadLeft(8, '0').Select(bit => bit == '1').ToList();
                result.AddRange(tmp);
            }

            Console.WriteLine($"[ReadFileToBoolArray] Count readed = {result.Count}\n");

            return result;
        }
    }
}
