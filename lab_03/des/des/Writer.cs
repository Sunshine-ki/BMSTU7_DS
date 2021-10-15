using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace des
{
    public static class Writer
    {
        public static void Write(List<List<bool>> blocks, string path, int addCount)
        {
            FileStream writer = File.OpenWrite(path);

            int max = 8;
            for (int k = 0; k < blocks.Count; k++)
            {
                if (k == blocks.Count - 1 && addCount > 0)
                {
                    max = (64 - addCount) / 8;
                    //Console.WriteLine($"max = {max}, addCount = {addCount}");
                }
                for (int i = 0; i < max; i++)
                {
                    var currentListByte = blocks[k].Skip(i * 8).Take(8).ToList();
                    var currentByte = DataOperations.listBoolConvertToInt(currentListByte);
                    //Console.WriteLine($"currentByte = {currentByte}");
                    writer.WriteByte((byte) currentByte);
                }
            }

            writer.Close();
        }

    }
}
