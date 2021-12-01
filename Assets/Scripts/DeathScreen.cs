using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    private void OnEnable()
    {
        ThirdPersonAim.FollowMouse = false;
        Time.timeScale = 0;
    }
    private void OnDestroy()
    {
        Time.timeScale = 1;
        ThirdPersonAim.FollowMouse = false;
    }
}
