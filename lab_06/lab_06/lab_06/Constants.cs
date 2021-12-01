using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace lab_06
{
    public class Constants
    {
        public static string PathToFolder = @"C:\Users\ASukocheva\source\repos\BMSTU7_DS\lab_06\lab_06\lab_06\data\";
        public static string PathToData = Path.Combine(PathToFolder, "simple_text.txt");
        public static string PathToCompressedData = Path.Combine(PathToFolder, "compress_data.txt");
        public static string PathToDecompressedData = Path.Combine(PathToFolder,"decompress_data.txt");
        public static string PathToMeta = Path.Combine(PathToFolder, "meta.txt");
    }
}
