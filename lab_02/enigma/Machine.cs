using System;
using System.Collections.Generic;

namespace Enigma
{
    public class Machine
    {
        private Roter _leftRoter;
        private Roter _middleRoter;
        private Roter _rightRoter;
        private Commutator _commutator;

        public Machine()
        {
            _leftRoter = new Roter();
            _middleRoter = new Roter();
            _rightRoter = new Roter();
            _commutator = new Commutator();

            _leftRoter.Shuffle();
            _middleRoter.Shuffle();
            _rightRoter.Shuffle();

            _commutator.GenerateCommutator();
        }

        public int Encode(int symbol)
        {
            int currentValue = _leftRoter.GetValueByIndex(symbol);
            currentValue = _middleRoter.GetValueByIndex(currentValue);
            currentValue = _rightRoter.GetValueByIndex(currentValue);

            currentValue = _commutator.Get(currentValue);

            currentValue = _rightRoter.GetIndexByValue(currentValue);
            currentValue = _middleRoter.GetIndexByValue(currentValue);
            currentValue = _leftRoter.GetIndexByValue(currentValue);

            if (_rightRoter.LeftShift())
            {
                if (_middleRoter.LeftShift())
                {
                    _leftRoter.LeftShift();
                }
            }

            return currentValue;
        }

        public void SetPosition(int? leftIndex = null, int? middleIndex = null, int? rightIndex = null)
        {
            if (leftIndex != null)
            {
                _leftRoter.Index = Convert.ToInt32(leftIndex);
            }
            if (middleIndex != null)
            {
                _middleRoter.Index = Convert.ToInt32(middleIndex);
            }
            if (rightIndex != null)
            {
                _rightRoter.Index = Convert.ToInt32(rightIndex);
            }
        }

        public List<int> GetPosition()
        {
            return new List<int>() { _leftRoter.Index, _middleRoter.Index, _rightRoter.Index };
        }
    }
}
