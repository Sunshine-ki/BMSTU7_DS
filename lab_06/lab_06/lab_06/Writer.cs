using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace lab_06
{
    public static class Writer
    {
        public static void WriteBoolArrayToFile(List<bool> data, string path)
        {
            // Чтобы данные не перезаписывались.
            // (могут наслоиться друг на друга и может остаться часть старых данных).
            //if (File.Exists(path)) File.Delete(path);

            using var writer = File.OpenWrite(path);
            for (int k = 0; k < data.Count; k += 8)
            {
                var currentByte = Converter.ConvertListBoolToInt(data.Skip(k).Take(8).ToList());
                writer.WriteByte((byte)currentByte);
            }
        }
    }
}
