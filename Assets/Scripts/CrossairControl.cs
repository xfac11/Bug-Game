using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossairControl : MonoBehaviour
{
    [SerializeField] private ThirdPersonAim ThirdPersonAim;

    [SerializeField] private GameObject AimedCrossAir;
    [SerializeField] private GameObject NormalCrossAir;
    [SerializeField] private GameObject HitMark;
    // Start is called before the first frame update
    private void OnEnable()
    {
        AIEnemy.OnHit += HitEnemy;
        ThirdPersonAim.Aimed += DisplayCrossAir;
        ThirdPersonAim.NoAim += DisplayNormalCrossAir;
    }

    private void HitEnemy()
    {
        StartCoroutine(ShowHitMark());
    }

    private IEnumerator ShowHitMark()
    {
        HitMark.SetActive(true);
        yield return new WaitForSeconds(0.01f);
        HitMark.SetActive(false);
    }

    private void OnDisable()
    {
        ThirdPersonAim.Aimed -= DisplayCrossAir;
        ThirdPersonAim.NoAim -= DisplayNormalCrossAir;
    }

    private void DisplayNormalCrossAir()
    {
        if(AimedCrossAir != null)
            AimedCrossAir.SetActive(false);
        if (NormalCrossAir != null)
            NormalCrossAir.SetActive(true);
    }

    private void DisplayCrossAir()
    {
        if (AimedCrossAir != null)
            AimedCrossAir.SetActive(true);
        if (NormalCrossAir != null)
            NormalCrossAir.SetActive(false);
    }
}
