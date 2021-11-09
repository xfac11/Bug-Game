using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
namespace Bug.Controls
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Camera Camera;
        [SerializeField] private CinemachineVirtualCamera NormCam;
        [SerializeField] private CinemachineVirtualCamera AimCam;
        [SerializeField] private GameObject CrossAir;
        int _defaultPriority;

        private void Start()
        {
            _defaultPriority = AimCam.Priority;
        }
        void Update()
        {
            if (Input.GetMouseButton(1))
            {
                AimCam.Priority = _defaultPriority + 2;
                CrossAir.SetActive(true);//move to own class with events
            }
            else
            {
                AimCam.Priority = _defaultPriority;
                CrossAir.SetActive(false);
            }
        }
    }
}