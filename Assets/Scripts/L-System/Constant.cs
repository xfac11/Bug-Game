using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Bug.L_System
{
    [System.Serializable]
    public class Constant
    {
        public char _a;
        public string _strA;
        public Constant(char constant)
        {
            A = constant;
        }
        /// <summary>
        /// Will set the constant char and the constant string
        /// </summary>
        public char A
        {
            get
            {
                return _a;
            }
            set
            {
                _a = value;
                _strA = value.ToString();
            }
        }
        public string AString
        {
            get
            {
                return _strA;
            }
        }
        public void CheckConstant(char current, StringBuilder stringBuilder)
        {
            if(current == _a)
            {
                stringBuilder.Append(_strA);
            }
        }
    }
}

