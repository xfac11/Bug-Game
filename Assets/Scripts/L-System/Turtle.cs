using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Bug.L_System
{
    public class Turtle : MonoBehaviour
    {
        private LSystem _lSystem;
        private List<IInstruction> _instructions;
        private Dictionary<char, Action> _mapping;
        private string _alphabet;
        private string _current;
        private int _walkIndex;
        private Mesh mesh;
        [SerializeField] private string Axium;
        [SerializeField] private List<Rule> Rules;
        [SerializeField] private List<Constant> Constants;
        [SerializeField] private int Generations;
        [SerializeField] private string Alphabet;


        private void Awake()
        {
            mesh = new Mesh();
            _mapping = new Dictionary<char, Action>();
            _instructions = new List<IInstruction>();
            _lSystem = new LSystem();
            IInstruction[] instructions = GetComponents<IInstruction>();
            if(instructions.Length != 0)
            {
                _instructions.AddRange(instructions);
            }
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < _instructions.Count; i++)
            {
                stringBuilder.Append(_instructions[i].Symbol);
            }
            _alphabet = stringBuilder.ToString();
            GenerateMappingOfAlphabet();
            PrepareLSystem();
            _current = _lSystem.Generate(Generations);
            Debug.Log(_current);
        }
        public void GenerateMappingOfAlphabet()
        {
            if(_alphabet.Length == 0 || _instructions.Count == 0)
            {
                Debug.LogError("Alphabet or instructions are empty. Cannot generate mapping from an empty alphabet or with no instructions");
                return;
            }
            foreach (var item in _instructions)
            {
                _mapping.Add(item.Symbol, item.Operation);
            }
        }
        

        private void PrepareLSystem()
        {
            foreach (var item in Rules)
            {
                _lSystem.AddRule(item.A, item.B);
            }
            foreach (var item in Constants)
            {
                _lSystem.AddConstant(item.A);
            }
            _lSystem.SetAxiom(Axium);
        }
        public void Walk()
        {
            _mapping[_current[_walkIndex]].Invoke();
            _walkIndex++;
            if(_walkIndex > _current.Length)
            {
                _walkIndex = 0;
            }
        }
        public void WalkTheSystem()
        {
            LineRenderer lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.enabled = true;
            StartCoroutine(WalkToEnd());
            //mesh = new Mesh();
            //lineRenderer.BakeMesh(mesh);
            //lineRenderer.enabled = false;
            //GetComponent<MeshFilter>().mesh = mesh;
        }

        private IEnumerator WalkToEnd()
        {
            bool going = true;
            while (going)
            {
                _mapping[_current[_walkIndex]].Invoke();
                _walkIndex++;
                if (_walkIndex >= _current.Length)
                {
                    _walkIndex = 0;
                    going = false;
                }
                yield return null;
            }
        }
    }
}