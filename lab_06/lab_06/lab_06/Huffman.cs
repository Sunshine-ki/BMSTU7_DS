﻿using System;
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
    }
}
