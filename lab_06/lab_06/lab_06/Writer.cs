using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace lab_06
{
    public static class Writer
    {
        // Если data не кратна 8, то в конце запишутся нолики.
        public static void WriteBoolArrayToFile(List<bool> data, string path)
        {
            if (File.Exists(path)) File.Delete(path);

            int max = data.Count;
            bool flag = false;
            if (data.Count % 8 != 0)
            {
                flag = true;
                max = data.Count / 8 * 8;
                //Console.WriteLine($"[WriteCompressedData] data.Count % 8  ({data.Count}) != 0");
            }

            using var writer = File.OpenWrite(path);
            for (int k = 0; k < max; k += 8)
            {
                var currentByte = Converter.ConvertListBoolToInt(data.Skip(k).Take(8).ToList());
                writer.WriteByte((byte)currentByte);
            }

            if (flag)
            {
                var t = data.Skip(max).Take(data.Count % 8).ToList();
                var p = 8 - data.Count % 8;
                for (int i = 0; i < p; i++)
                {
                    t.Add(false);
                }

                var currentByte = Converter.ConvertListBoolToInt(t);

                writer.WriteByte((byte)currentByte);
            }
        }

        public static void WriteByteArrayToFile(List<byte> data, string path)
        {
            if (File.Exists(path)) File.Delete(path);

            using var writer = File.OpenWrite(path);
            data.ForEach(elem => { writer.WriteByte(elem); });
        }
    }
}
