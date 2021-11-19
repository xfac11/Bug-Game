using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Bug.L_System
{
    public class LSystem
    {
        private List<Rule> _rules = new List<Rule>();
        private List<Constant> _constants = new List<Constant>();
        private StringBuilder _current = new StringBuilder();
        private string _axium;
        public void AddConstant(char constant)
        {
            Constant newConstant = new Constant(constant);
            _constants.Add(newConstant);
        }
        /// <summary>
        /// predecessor becomes successor
        /// </summary>
        /// <param name="predecessor"></param>
        /// <param name="successor"></param>
        public void AddRule(char predecessor, string successor)
        {
            Rule rule = new Rule(predecessor, successor);
            _rules.Add(rule);
        }
        public void SetAxiom(string axium)
        {
            _axium = axium;
        }
        public string Generate(int generations)
        {
            _current.Clear();
            _current.Append(_axium);
            for (int k = 0; k < generations; k++)
            {
                StringBuilder strBuilder = new StringBuilder();
                string currentStr = _current.ToString();

                for (int i = 0; i < _current.Length; i++)
                {
                    char currentChar = currentStr[i];
                    for (int j = 0; j < _rules.Count; j++)
                    {
                        _rules[j].CheckRule(currentChar, strBuilder);
                    }
                    for (int j = 0; j < _constants.Count; j++)
                    {
                        _constants[j].CheckConstant(currentChar, strBuilder);
                    }
                }
                _current = strBuilder;
            }
            return _current.ToString();
        }
    }
}