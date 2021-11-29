using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public void SetActive(bool activate, float delay)
    {
        if(activate)
        {
            gameObject.SetActive(true);
            LeanTween.scale(gameObject, new Vector3(0.5f,0.5f,1f), delay).setEaseInBounce().setEaseSpring();
        }
        else
        {
            LeanTween.scale(gameObject, new Vector3(0.0f, 0.0f, 1f), delay).setEaseInBounce().setEaseSpring();
            StartCoroutine(DelayDeactivation(delay));
        }
    }
    IEnumerator DelayDeactivation(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        gameObject.SetActive(false);
    }
}
