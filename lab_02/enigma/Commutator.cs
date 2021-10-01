using System;
using System.Collections.Generic;

namespace Enigma
{
    public class Commutator
    {
        private List<int> _commutator;

        public Commutator()
        {
            _commutator = new List<int>(Constants.MAX_INDEX_VALUE / 2);
        }

        public void GenerateCommutator()
        {
            for (int i = Constants.MAX_INDEX_VALUE / 2; i < Constants.MAX_INDEX_VALUE; i++)
            {
                _commutator.Add(i);
            }
        }

        public int Get(int value)
        {
            if (value < Constants.MAX_INDEX_VALUE / 2)
            {
                return _commutator[value];
            }
            return _commutator.FindIndex(elem => elem == value);
        }

        public override string ToString()
        {
            return String.Join(" ", _commutator) + "\n\n";
        }
    }
}
