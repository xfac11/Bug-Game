using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonAim : MonoBehaviour
{
    public Cinemachine.CinemachineVirtualCamera ThirdPersonCamera;
    public Cinemachine.CinemachineVirtualCamera AimCamera;
    public event Action Aimed;
    public event Action NoAim;

    public float NormalFOV = 40f;
    public float AimedFOV = 20f;
    public float duration = 1.0f;
    private float _timer = 0.0f;
    private CinemachineBasicMultiChannelPerlin _cinemachineBasicMultiChannelPerlin;

    private void Awake()
    {
        _cinemachineBasicMultiChannelPerlin = ThirdPersonCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }
    private void Update()
    {
        if (Time.timeScale == 0)
        {
            ThirdPersonCamera.gameObject.SetActive(false);
            AimCamera.gameObject.SetActive(false);
            return;
        }
        if(ThirdPersonCamera.gameObject.activeSelf == false)
        {
            ThirdPersonCamera.gameObject.SetActive(true);
            AimCamera.gameObject.SetActive(true);
        }
            
        if (Input.GetMouseButtonDown(1))
        {
            //StopAllCoroutines();
            //StartCoroutine(ChangeFOV(AimedFOV));
            ActivateAim();
        }
        if(Input.GetMouseButtonUp(1))
        {
            ActivateDefaultCamera();
            //StopAllCoroutines();
            //StartCoroutine(ChangeFOV(NormalFOV));
        }
        if(_timer > 0)
        {
            _timer -= Time.deltaTime;
            if(_timer <= 0f)
            {
                _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0.0f;
            }
        }
    }
    public void CameraShake(float time, float amplitudeGain)
    {
        _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = amplitudeGain;
        _timer = time;
    }
    private void ActivateAim()
    {
        Aimed?.Invoke();
        AimCamera.Priority += 10;
    }

    private void ActivateDefaultCamera()
    {
        NoAim?.Invoke();
        AimCamera.Priority -= 10;
    }


    IEnumerator ChangeFOV(float angle)
    {
        float currentFOV = ThirdPersonCamera.m_Lens.FieldOfView;
        float timeElapsed = 0;
        while(timeElapsed < duration)
        {
            float newAngle = Mathf.Lerp(currentFOV, angle, timeElapsed/duration);
            ThirdPersonCamera.m_Lens.FieldOfView = newAngle;
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        ThirdPersonCamera.m_Lens.FieldOfView = angle;
    }
}
