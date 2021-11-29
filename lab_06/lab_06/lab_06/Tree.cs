using System;
using System.Collections.Generic;
using System.Text;

namespace lab_06
{
    public class Tree
    {
        private KeyValuePair<char?, int> _data;
        private Tree _left;
        private Tree _right;

        public Tree Left { get => _left; set { _left = value; } }
        public Tree Right { get => _right; set { _right = value; } }
        public KeyValuePair<char?, int> Data { get => _data; set { _data = value; } }

        // Create node
        public Tree(int data, Tree left, Tree right)
        {
            _data = new KeyValuePair<char?, int>(null, data);
            _left = left;
            _right = right;
        }

        // Create leaf
        public Tree(KeyValuePair<char?, int> data)
        {
            _data = data;
            _left = null;
            _right = null;
        }

        public Tree(int data)
        {
            _data = new KeyValuePair<char?, int>(null, data);
            _left = null;
            _right = null;
        }

        public bool IsLeaf()
        {
            return _left is null && _right is null;
        }

        public bool DataIsNull()
        {
            return _data.Key is null;
        }

    }
}
