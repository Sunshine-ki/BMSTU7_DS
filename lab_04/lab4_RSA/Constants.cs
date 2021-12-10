using System;
using System.IO;

namespace lab_04
{
    static class Constants
    {
        public static string PathToData = "../../data/";
        public static string FilenameIN = Path.Combine(PathToData, "test.txt");
        public static string FilenameEncr = Path.Combine(PathToData, "test_encr.txt");
        public static string FilenameDecr = Path.Combine(PathToData, "test_decrypted.txt");
    }
}
