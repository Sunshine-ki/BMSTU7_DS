using System;
using System.IO;

namespace lab_04
{
    public class FileOperations
    {
        /// Побайтная обработка файла
        public static void ProcessFileToBinary(string pathToSrc, string pathToDst, Func<int, int> ProcessOneInt)
        {
            FileStream fsSrc = new FileStream(pathToSrc, FileMode.Open);
            FileStream fsDst = new FileStream(pathToDst, FileMode.Create);
            BinaryWriter binWriter = new BinaryWriter(fsDst);

            int cur;
            while (fsSrc.CanRead)
            {
                cur = fsSrc.ReadByte();
                if (cur == -1)
                    break;
                int res = ProcessOneInt(cur);
                //Console.WriteLine($"cur: {cur} res:{res}");
                binWriter.Write(res);
            }

            binWriter.Write(-1);
            binWriter.Close();
            fsDst.Close();
            fsSrc.Close();
        }

        public static void ProcessBinaryToNormal(string src, string dst, Func<int, int> ProcessOneInt)
        {
            FileStream fsSrc = new FileStream(src, FileMode.Open);
            FileStream fsDst = new FileStream(dst, FileMode.Create);
            BinaryReader binReader = new BinaryReader(fsSrc);

            int cur;
            while (fsSrc.CanRead)
            {
                cur = binReader.ReadInt32();
                if (cur == -1)
                    break;
                int res = ProcessOneInt(cur);
                //Console.WriteLine($"cur: {cur} res:{res}");
                fsDst.WriteByte((byte)res);
            }

            binReader.Close();
            fsDst.Close();
            fsSrc.Close();
        }
    }
}
