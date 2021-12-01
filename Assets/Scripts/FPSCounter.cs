using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    private TMPro.TMP_Text _text;
    private int avgFrameRate;

    private void Awake()
    {
        _text = GetComponent<TMPro.TMP_Text>();

    }

    public void Update()
    {
        float current = 0;
        current = (int)(1f / Time.unscaledDeltaTime);
        avgFrameRate = (int)current;
        _text.text = avgFrameRate.ToString();
    }
}
