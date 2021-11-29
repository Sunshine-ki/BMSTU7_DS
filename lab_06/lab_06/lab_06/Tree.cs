using System;
using System.Collections.Generic;
using System.Text;

namespace lab_06
{
    public class Tree : IComparable<Tree>
    {
        private KeyValuePair<byte?, int> _data;
        private Tree _left;
        private Tree _right;

        public Tree Left { get => _left; set { _left = value; } }
        public Tree Right { get => _right; set { _right = value; } }
        public KeyValuePair<byte?, int> Data { get => _data; set { _data = value; } }

        #region Constructors
        // Create node
        public Tree(int data, Tree left, Tree right)
        {
            _data = new KeyValuePair<byte?, int>(null, data);
            _left = left;
            _right = right;
        }

        // Create leaf
        public Tree(byte? symbol, int count)
        {
            _data = new KeyValuePair<byte?, int>(symbol, count);
            _left = null;
            _right = null;
        }

        // Create leaf
        public Tree(KeyValuePair<byte?, int> data)
        {
            _data = data;
            _left = null;
            _right = null;
        }
        #endregion

        public bool IsLeaf()
        {
            return _left is null && _right is null;
        }

        public bool DataIsNull()
        {
            return _data.Key is null;
        }

        #region Height
        public int GetHeight()
        {
            int height = 0, maxHeight = 0;

            getHeight(this, ref height, ref maxHeight);

            return maxHeight;
        }

        private void getHeight(Tree tree, ref int height, ref int maxHeight)
        {
            if (tree.Left == null && tree.Right == null)
            {
                if (height > maxHeight)
                {
                    maxHeight = height;
                }
                return;
            }

            if (!(tree.Left is null))
            {
                height++;
                getHeight(tree.Left, ref height, ref maxHeight);
                height--;
            }

            if (!(tree.Right is null))
            {
                height++;
                getHeight(tree.Right, ref height, ref maxHeight);
                height--;
            }
        }
        #endregion

        public int CompareTo(Tree other) 
        { 
            return _data.Value.CompareTo(other.Data.Value); 
        }

        public override string ToString()
        {
            return $"{_data.Key} = {_data.Value}";
        }
    }
}
