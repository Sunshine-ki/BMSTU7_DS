using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace des
{
    public static class Reader
    {
        public static List<List<bool>> Read(string path, ref int addCount)
        {
            FileStream stream = File.OpenRead(path);

            List<List<bool>> blocks = new List<List<bool>>();
            List<bool> currentBlock = new List<bool>();

            int elem;
            while ((elem = stream.ReadByte()) > -1)
            {
                byte b = (byte)elem;
                currentBlock.AddRange(Convert.ToString(b, 2).PadLeft(8, '0').Select(bit => bit == '1').ToList());

                if (currentBlock.Count >= 64)
                {
                    blocks.Add(currentBlock);
                    currentBlock = new List<bool>();
                }

                //Console.WriteLine(b);
            }
            stream.Close();

            addCount = 0;
            if (currentBlock.Count != 0)
            {
                addCount = 64 - currentBlock.Count;
                for (int i = 0; i < addCount; i++)
                {
                    currentBlock.Add(false);
                }
                blocks.Add(currentBlock);

                //currentBlock = new List<bool>();
                //currentBlock.AddRange(Convert.ToString(addCount, 2).PadLeft(8, '0').Select(bit => bit == '1').ToList());
                //while (currentBlock.Count < 64)
                //{
                //    currentBlock.Add(false);
                //}
                //blocks.Add(currentBlock);
            }

            //Console.WriteLine($"len blocks = {blocks.Count}");
            //Console.WriteLine($"len currentBlock = {currentBlock.Count}");

            return blocks;
        }

    }
}
