using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedText : MonoBehaviour
{
    public string Text { get; set; }
    void Start()
    {
        GetComponent<TMPro.TMP_Text>().text = Text;
        LTDescr descr = LeanTween.moveY(gameObject, transform.localPosition.y + 200, 1.0f).setOnComplete( () => { Destroy(gameObject); });
    }
}
