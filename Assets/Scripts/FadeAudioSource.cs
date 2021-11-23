using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FadeAudioSource
{
    public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume, bool shouldStop)
    {
        float currentTime = 0;
        float start = audioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        if(shouldStop)
        {
            audioSource.Stop();
        }
        yield break;
    }
}
