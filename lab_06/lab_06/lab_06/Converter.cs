using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lab_06
{
    public static class Converter
    {
        public static List<byte> ConvertListBoolToListByte(List<bool> data)
        {
            var result = new List<byte>();

            for (int i = 0; i < data.Count; i += 8)
            {
                int currentByte = ConvertListBoolToInt(data.Skip(i).Take(8).ToList());
                result.Add((byte)currentByte);
            }

            return result;
        }

        public static int ConvertListBoolToInt(List<bool> data)
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

        public static List<bool> ConvertIntToBitArray(int number, int lenght)
        {
            var numberBitArray = System.Convert.ToString(number, 2).PadLeft(lenght, '0').Select(bit => bit == '1').ToList();
            var numberByteArray = ConvertListBoolToListByte(numberBitArray);

            return numberBitArray;
        }
    }
}
