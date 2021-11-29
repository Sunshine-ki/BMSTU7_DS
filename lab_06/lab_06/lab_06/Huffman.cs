using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MoreLinq;

namespace lab_06
{
    public class Huffman
    {
        public Dictionary<byte, int> CreateWeights(List<byte> data)
        {
            var weights = new Dictionary<byte, int>();

            data.ForEach(elem =>
            {
                if (!weights.ContainsKey(elem))
                {
                    weights.Add(elem, data.Count(x => x.Equals(elem)));
                }
            });

            return weights;
        }

        public Tree CreateTree(Dictionary<byte, int> weights)
        {
            var result = new List<Tree>();

            weights.ForEach(weight =>
                { result.Add(new Tree(weight.Key, weight.Value)); });


            while (result.Count != 1)
            {
                var firstMin = result.Min();
                result.Remove(firstMin);

                var secondMin = result.Min();
                result.Remove(secondMin);

                result.Add(new Tree(firstMin.Data.Value + secondMin.Data.Value, firstMin, secondMin));
            }

            return result[0];
        }

        public Dictionary<byte, List<bool>> CreateSubstitutions(Tree tree)
        {
            var result = new Dictionary<byte, List<bool>>();
            var currentByte = new List<bool>();

            createSubstitutions(tree, currentByte, result);

            return result;
        }

        private void createSubstitutions(Tree tree, List<bool> currentByte, Dictionary<byte, List<bool>> result)
        {
            if (tree.Left is null && tree.Right is null)
            {
                var b = currentByte.GetRange(0, currentByte.Count);

                if (tree.Data.Key is null) throw new Exception("[createSubstitutions] tree.Data.Key is null");
                result.Add((byte)tree.Data.Key, b);

                return;
            }

            if (tree.Left != null)
            {
                currentByte.Add(false);
                createSubstitutions(tree.Left, currentByte, result);
                currentByte.RemoveAt(currentByte.Count - 1);
            }

            if (tree.Right != null)
            {
                currentByte.Add(true);
                createSubstitutions(tree.Right, currentByte, result);
                currentByte.RemoveAt(currentByte.Count - 1);
            }
        }

        public List<bool> Replace(List<byte> data, Dictionary<byte, List<bool>> substitutions)
        {
            var result = new List<bool>();

            foreach (var symbol in data)
            {
                result.AddRange(substitutions[symbol]);
            }

            return result;
        }

        // Return size in bit
        public int GetMetaSize(Dictionary<byte, List<bool>> substitutions)
        {
            int size = 0; // 32 bit

            // Длина каждого заменяемого кода.
            substitutions.ForEach(sub =>  { size += sub.Value.Count; });

            // 2 байта под каждую замену.
            // 1ый байт - [0-255] какой символ
            // 2ой байт - [0-255] размер заменяемого кода
            size += 2 * substitutions.Count * 8;

            return size;
        }

        public List<bool> CreateMeta(Dictionary<byte, List<bool>> substitutions)
        {
            var result = new List<bool>();

            var metaSize = GetMetaSize(substitutions);
            result.AddRange(Converter.ConvertIntToBitArray(metaSize, 32));

            foreach (var sub in substitutions)
            {
                result.AddRange(Converter.ConvertIntToBitArray(sub.Key, 8));
                result.AddRange(Converter.ConvertIntToBitArray(sub.Value.Count, 8));
                result.AddRange(sub.Value);
            }

            while (result.Count % 8 != 0)
            {
                result.Add(false);
            }

            return result;
        }

        public Dictionary<byte, List<bool>> ConvertListBitToSubstitutions(List<bool> data)
        {
            var result = new Dictionary<byte, List<bool>>();
            List<bool> currentCode = new List<bool>();

            int metaSize = Converter.ConvertListBoolToInt(data.Take(32).ToList());
            Console.WriteLine($"[ConvertListBitToSubstitutions] metaSize = {metaSize}");


            var realData = data.Skip(32).Take(metaSize).ToList();

            for (int i = 0; i < realData.Count;)
            {
                var symbol = Converter.ConvertListBoolToInt(realData.Skip(i).Take(8).ToList());
                i += 8;

                var codeSize = Converter.ConvertListBoolToInt(realData.Skip(i).Take(8).ToList());
                i += 8;

                for(int j = 0; j < codeSize; j++)
                {
                    currentCode.Add(realData[i]);
                    i++;
                }

                result.Add((byte)symbol, currentCode.GetRange(0, currentCode.Count));
                currentCode.Clear();
            }

            return result;
        }
    }
}
