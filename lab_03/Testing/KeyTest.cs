using System;
using Xunit;
using des;

namespace Testing
{
    public class KeyTest
    {
        [Fact]
        public void LenKey()
        {
            string keyString = "12345678";

            var key = Key.ConvertStringToBoolList(keyString);

            Assert.Equal(64, key.Count);
        }
        
        [Fact]
        public void LenKeys()
        {
            string keyString = "12345678";

            var key = Key.ConvertStringToBoolList(keyString);
            var keys = Key.Generate(key);

            Assert.Equal(16, keys.Count);
            foreach(var elem in keys)
            {
                Assert.Equal(48, elem.Count);
            }
        }
    }
}
