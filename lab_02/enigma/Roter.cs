using System;
using System.Collections.Generic;
using System.Linq;

namespace Enigma
{
    public class Roter
    {
        private List<int> _roter;
        private int _beginIndex;
        
        public Roter()
        {
            _roter = Enumerable.Range(0, Constants.MAX_INDEX_VALUE).ToList();
            _beginIndex = 0;
        }

        public int Index
        {
            get => _beginIndex;
            set
            {
                if (value > Constants.MAX_INDEX_VALUE)
                {
                    return;
                }

                _beginIndex = value;
            }
        }

        public int GetValueByIndex(int index)
        {
            if (index + _beginIndex < Constants.MAX_INDEX_VALUE)
            {
                return _roter[index + _beginIndex];
            }
            return _roter[(index + _beginIndex) % Constants.MAX_INDEX_VALUE];
        }

        public int GetIndexByValue(int value)
        {
            int index = _roter.FindIndex(elem => elem == value);

            if (index - _beginIndex < 0)
            {
                return Constants.MAX_INDEX_VALUE + (index - _beginIndex);
            }
            return index - _beginIndex;
        }

        public void Shuffle()
        {
            Random rand = new Random();

            for (int i = _roter.Count - 1; i >= 1; i--)
            {
                int j = rand.Next(i + 1);

                int tmp = _roter[j];
                _roter[j] = _roter[i];
                _roter[i] = tmp;
            }
        }

        public bool LeftShift()
        {
            if (_beginIndex + 1 >= Constants.MAX_INDEX_VALUE)
            {
                _beginIndex = 0;
                return true;
            }
            _beginIndex++;
            return false;
        }

        public override string ToString()
        {
            return String.Join(" ", _roter) + "\n\n";
        }
    }
}
