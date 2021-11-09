using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bug.Camera
{
    public class ThirdPersonCamera : MonoBehaviour
    {
        [SerializeField] private Transform Target;
        [Range(0f, 100f)]
        [SerializeField] private float DistanceFromTarget = 2f;
        [Range(0, 20f)]
        [SerializeField] private float MouseSensitivity = 10f;
        [Range(-45f, 0f)]
        [SerializeField] private float PitchMin = -45;
        [Range(0f, 90f)]
        [SerializeField] private float PitchMax = 90;
        [Range(0f, 1f)]
        [SerializeField] private float RotationSmoothTime = 0.12f;
        [SerializeField] private bool LockCursor = false;
        private Vector3 rotationSmoothVelocity;
        private Vector3 currentRotaion;
        private float _yaw;
        private float _pitch;
        private void Awake()
        {
            if(LockCursor)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
        private void LateUpdate()
        {
            _yaw += Input.GetAxis("Mouse X") * MouseSensitivity;
            _pitch -= Input.GetAxis("Mouse Y") * MouseSensitivity;
            _pitch = Mathf.Clamp(_pitch, PitchMin, PitchMax);

            currentRotaion = Vector3.SmoothDamp(currentRotaion, new Vector3(_pitch, _yaw), ref rotationSmoothVelocity, RotationSmoothTime);
            transform.eulerAngles = currentRotaion;

            transform.position = Target.position - transform.forward * DistanceFromTarget;
        }
    }
}