using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Bug.L_System
{
    [System.Serializable]
    public class Rule
    {
        public char _predecessor;
        public string _successor;

        public Rule(char predecessor, string successor)
        {
            _predecessor = predecessor;
            _successor = successor;
        }
        public char A
        {
            set
            {
                _predecessor = value;
            }
            get
            {
                return _predecessor;
            }
        }
        public string B
        {
            set
            {
                _successor = value;
            }
            get
            {
                return _successor;
            }
        }
        public void SetRule(char A, string B)
        {
            _predecessor = A;
            _successor = B;
        }
        public void CheckRule(char current, StringBuilder stringBuilder)
        {
            if(current == _predecessor)
            {
                stringBuilder.Append(_successor);
            }
        }
    }
}