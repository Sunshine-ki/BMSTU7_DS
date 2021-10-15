using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace des
{
    public static class DataOperations
    {
        public static List<bool> Permutate(List<bool> data, List<int> indexes)
        {
            List<bool> result = new List<bool>();

            foreach (var index in indexes)
            {
                result.Add(data[index]);
            }

            return result;
        }

        public static int listBoolConvertToInt(List<bool> data)
        {
            int result = 0;

            for (int i = data.Count - 1, k = 0; i >= 0; i--, k++)
            {
                if (data[i])
                {
                    result += Convert.ToInt32(Math.Pow(2, k));
                }
            }

            return result;
        }

        public static List<bool> FeistelFunction(List<bool> mr, List<bool> key) // mr - msg right
        {
            var result = new List<bool>();

            var z = Xor(Permutate(mr, Constants.E), key);

            for (int k = 0; k < 8; k++)
            {
                var current = z.Skip(k * 6).Take(6).ToList();

                var i = listBoolConvertToInt(new List<bool>() { current[0], current[5] });
                var j = listBoolConvertToInt(current.Skip(1).Take(4).ToList());

                var value = Constants.Sblocks[k][i * 16 + j];
                
                var newValues = Convert.ToString(value, 2).PadLeft(4, '0').Select(bit => bit == '1').ToList();
                result.AddRange(newValues);
            }

            Permutate(result, Constants.P);

            return result;
        }

        private static List<bool> getLeftData(List<bool> data)
        {
            List<bool> result = new List<bool>();

            for (int i = 0; i < 32; i++)
            {
                result.Add(data[i]);
            }

            return result;
        }

        private static List<bool> getRightData(List<bool> data)
        {
            List<bool> result = new List<bool>();

            for (int i = 32; i < 64; i++)
            {
                result.Add(data[i]);
            }

            return result;
        }

        public static List<bool> mergeList(List<bool> first, List<bool> second)
        {
            List<bool> result = new List<bool>(first);
            result.AddRange(second);
            return result;
        }

        private static List<bool> Xor(List<bool> arrayFirst, List<bool> arraySecond)
        {
            if (arrayFirst.Count != arraySecond.Count)
            {
                throw new Exception($"Count elem must match. {arrayFirst.Count} and {arraySecond.Count}");
            }

            List<bool> result = new List<bool>();

            for (int i = 0; i < arrayFirst.Count; i++)
            {
                result.Add(arrayFirst[i] ^ arraySecond[i]);
            }    

            return result;
        }

        public static List<bool> Encrypt(List<bool> data, List<List<bool>> keys)
        {
            var result = Permutate(data, Constants.IP);

            for (int i = 0; i < keys.Count; i++)
            {
                var leftData = getLeftData(result);
                var rightData = getRightData(result);

                var newLeft = new List<bool>(rightData);
                var newRight = Xor(leftData, FeistelFunction(rightData, keys[i]));

                result = mergeList(newLeft, newRight);
            }

            result = Permutate(result, Constants.IPInverse);

            return result;
        }

        public static List<bool> Decrypt(List<bool> data, List<List<bool>> keys)
        {
            var result = Permutate(data, Constants.IP);

            for (int i = keys.Count - 1; i >= 0 ; i--)
            {
                var leftData = getLeftData(result);
                var rightData = getRightData(result);

                var oldRight = new List<bool>(leftData);
                var oldLeft = Xor(rightData, FeistelFunction(leftData, keys[i]));

                result = mergeList(oldLeft, oldRight);
            }

            result = Permutate(result, Constants.IPInverse);

            return result;
        }
        

    }
}
