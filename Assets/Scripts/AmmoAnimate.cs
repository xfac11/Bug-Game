using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoAnimate : MonoBehaviour
{
    private void Start()
    {
        LeanTween.moveY(gameObject, transform.position.y + 0.5f, 0.5f).setEaseInOutSine().setLoopPingPong();
        LeanTween.rotateY(gameObject, 90, 2).setEaseInOutSine().setLoopPingPong();
    }
}
