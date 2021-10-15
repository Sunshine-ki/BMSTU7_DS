using System;
using Xunit;
using System.Collections.Generic;
using des;

namespace Testing
{
    public class Data
    {
        private List<bool> createData()
        {
            List<bool> data = new List<bool>();
            Random random = new Random();

            for (int i = 0; i < 64; i++)
            {
                data.Add(Convert.ToBoolean(random.Next(0,2)));
            }

            return data;
        }

        [Fact]
        public void EncryptionDecryption()
        {
            string keyString = "12345678";
            var key = Key.ConvertStringToBoolList(keyString);
            var keys = Key.Generate(key);
            List<bool> data = createData();

            var encryptedData = DataOperations.Encrypt(data, keys);
            var beginData = DataOperations.Decrypt(encryptedData, keys);

            for(int i = 0; i < data.Count; i++)
            {
                Assert.Equal(data[i], beginData[i]);
            }

        }
    }

}
