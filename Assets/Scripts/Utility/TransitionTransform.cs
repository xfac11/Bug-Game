using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bug.Utility
{
    public class TransitionTransform : MonoBehaviour
    {
        private Vector3 _transformPosition;
        private Vector3 _nextTransformPosition;
        private float _time;
        private bool coroutinRunning = false;
        public float Time
        {
            set => _time = value;
            get => _time;
        }
        public Vector3 StartPosition
        {
            set
            {
                _transformPosition = value;
            }
        }
        public Vector3 Transition
        {
            get
            {
                return _transformPosition;
            }
            set
            {
                _nextTransformPosition = value;
                if(coroutinRunning)
                {
                    StopCoroutine(StartTransition());
                    coroutinRunning = false;
                }
                StartCoroutine(StartTransition());
            }
        }
        IEnumerator StartTransition()
        {
            coroutinRunning = true;
            float distance = Vector3.Distance(_transformPosition, _nextTransformPosition);
            float velocity = distance*0.01f;
            while (distance != 0.0f)
            {
                _transformPosition = Vector3.MoveTowards(_transformPosition, _nextTransformPosition, velocity);
                distance = Vector3.Distance(_transformPosition, _nextTransformPosition);
                yield return new WaitForSeconds(0.1f);
            }
            yield return null;
        }

    }
}