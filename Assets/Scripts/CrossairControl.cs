using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossairControl : MonoBehaviour
{
    [SerializeField] private ThirdPersonAim ThirdPersonAim;

    [SerializeField] private GameObject AimedCrossAir;
    [SerializeField] private GameObject NormalCrossAir;
    // Start is called before the first frame update
    private void OnEnable()
    {
        ThirdPersonAim.Aimed += DisplayCrossAir;
        ThirdPersonAim.NoAim += DisplayNormalCrossAir;
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
