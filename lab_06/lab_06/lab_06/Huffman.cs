using System;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;

namespace lab_06
{
    public static class Huffman
    {
        public static List<bool> Compress(List<byte> dataByte, out int min, out int max)
        {
            var weights = createWeights(dataByte);

            var tree = createTree(weights);

            var substitutions = createSubstitutions(tree);
            
            min = substitutions.Select(x => x.Value.Count).Min();
            max = substitutions.Select(x => x.Value.Count).Max();

            var compressedData = replace(dataByte, substitutions);

            var dataSize = compressedData.Count; // bit
            var metaArrBit = Metadata.Create(substitutions, dataSize);
            Writer.WriteBoolArrayToFile(metaArrBit, Constants.PathToMeta);

            Console.WriteLine($"Metadata size for substitutions = {Metadata.GetMetaSize(substitutions)} (without the first int!)");

            Writer.WriteBoolArrayToFile(compressedData, Constants.PathToCompressedData);

            return compressedData;
        }

        #region Compress
        private static Dictionary<byte, int> createWeights(List<byte> data)
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

        private static Tree createTree(Dictionary<byte, int> weights)
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

            return result.FirstOrDefault();
        }

        private static Dictionary<byte, List<bool>> createSubstitutions(Tree tree)
        {
            var result = new Dictionary<byte, List<bool>>();
            var currentByte = new List<bool>();

            createSubstitutionsRec(tree, currentByte, result);

            return result;
        }

        private static void createSubstitutionsRec(Tree tree, List<bool> currentByte, Dictionary<byte, List<bool>> result)
        {
            if (tree.Left is null && tree.Right is null)
            {
                var b = currentByte.GetRange(0, currentByte.Count);

                if (tree.Data.Key is null) throw new Exception("[createSubstitutionsRec] tree.Data.Key is null");
                result.Add((byte)tree.Data.Key, b);

                return;
            }

            if (tree.Left != null)
            {
                currentByte.Add(false);
                createSubstitutionsRec(tree.Left, currentByte, result);
                currentByte.RemoveAt(currentByte.Count - 1);
            }

            if (tree.Right != null)
            {
                currentByte.Add(true);
                createSubstitutionsRec(tree.Right, currentByte, result);
                currentByte.RemoveAt(currentByte.Count - 1);
            }
        }

        private static List<bool> replace(List<byte> data, Dictionary<byte, List<bool>> substitutions)
        {
            var result = new List<bool>();

            foreach (var symbol in data)
            {
                result.AddRange(substitutions[symbol]);
            }

            return result;
        }
        #endregion

        public static List<byte> Decompress(Dictionary<byte, List<bool>> substitutions, List<bool> data)
        {
            var result = new List<byte>();
            var currentCode = new List<bool>();
            byte? currentSymbol;

            for (int i = 0; i < data.Count; i++)
            {
                currentCode.Add(data[i]);

                currentSymbol = findSubstitution(substitutions, currentCode);
                if (currentSymbol != null)
                {
                    result.Add((byte)currentSymbol);
                    currentCode.Clear();
                }
            }

            return result;
        }

        #region Decompress
        private static byte? findSubstitution(Dictionary<byte, List<bool>> substitutions, List<bool> currentCode)
        {
            byte? currentSymbol = null;

            foreach (var sub in substitutions)
            {
                if (sub.Value.SequenceEqual(currentCode))
                {
                    currentSymbol = sub.Key;
                    break;
                }
            }

            return currentSymbol;
        }
        #endregion

        #region Output
        public static void PrintWeights(Dictionary<byte, int> weights)
        {
            var orderedWeights = weights.OrderBy(w => w.Value);
            foreach (var elem in orderedWeights)
            {
                Console.WriteLine(string.Format("{0, 6} = {1,6}", elem.Key, elem.Value));
            }
        }

        public static void PrintSubstitutions(Dictionary<byte, List<bool>> substitutions)
        {
            Console.WriteLine("__________");

            // Ordered by code lenght.
            var orderedSubstitutions = substitutions.OrderBy(e => e.Value.Count);
            foreach (var elem in orderedSubstitutions)
            {
                Console.Write($"{elem.Key} ");
                elem.Value.ForEach(b => { Console.Write(b ? 1 : 0); });
                Console.Write("\n");
            }

            Console.WriteLine("__________");
        }
        #endregion
    }
}
