using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonAim : MonoBehaviour
{
    public Cinemachine.CinemachineFreeLook thirdPersonCamera;
    public float NormalFOV = 40f;
    public float AimedFOV = 20f;
    public float duration = 1.0f;
    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            StopAllCoroutines();
            StartCoroutine(ChangeFOV(AimedFOV));
        }
        if(Input.GetMouseButtonUp(1))
        {
            StopAllCoroutines();
            StartCoroutine(ChangeFOV(NormalFOV));
        }
    }

    IEnumerator ChangeFOV(float angle)
    {
        float currentFOV = thirdPersonCamera.m_Lens.FieldOfView;
        float timeElapsed = 0;
        while(timeElapsed < duration)
        {
            float newAngle = Mathf.Lerp(currentFOV, angle, timeElapsed/duration);
            thirdPersonCamera.m_Lens.FieldOfView = newAngle;
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        thirdPersonCamera.m_Lens.FieldOfView = angle;
    }
}
