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
            var result = new Dictionary<byte, int>();

            foreach(var elem in data)
            {
                if (!result.ContainsKey(elem))
                {
                    result.Add(elem, data.Count(x => x.Equals(elem)));
                }
            }

            return result;
        }

        public Dictionary<char, int> CreateWeights(string fileName)
        {
            var result = new Dictionary<char, int>();

            var allText = File.ReadAllText(fileName);
            foreach (var symbol in allText)
            {
                if (!result.ContainsKey(symbol))
                {
                    result.Add(symbol, allText.Count(x => x.Equals(symbol)));
                }
            }

            return result;
        }

        public KeyValuePair<char, int> GetMinAndDelete(Dictionary<char, int> dict)
        {
            var result = dict.MinBy(kvp => kvp.Value).First();
            dict.Remove(result.Key);
            return result;
        }

    }
}
