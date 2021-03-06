using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bug.Controls
{
    public class ThirdPersonMovement : MonoBehaviour
    {
        public CharacterController controller;
        public Transform cam;

        public float WalkingSpeed = 3f;
        public float RunningSpeed = 12f;

        private bool _canMove = true;
        private float _speed;
        public float turnSmoothTime = 0.1f;
        float turnSmoothVelocity;

        public void Stun(float seconds)
        {
            StartCoroutine(StopMoving(seconds));
        }

        private IEnumerator StopMoving(float seconds)
        {
            _canMove = false;
            yield return new WaitForSeconds(seconds);
            _canMove = true;
        }

        private void Start()
        {
            _speed = WalkingSpeed;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        private void Update()
        {
            if (Time.timeScale == 0)
                return;
            _speed = WalkingSpeed;
            if(Input.GetKey(KeyCode.LeftShift))
            {
                _speed = RunningSpeed;
            }
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            if(direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                if(_canMove)
                {
                    controller.Move(moveDirection.normalized * _speed * Time.deltaTime);
                }
            }

            //Need to add strafe
            if(Input.GetMouseButton(1))
            {
                float targetAngle = cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
            }
        }
    }
}