using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialSystem : MonoBehaviour
{
    [Serializable]
    struct TextEvents
    {
        public string textToShow;
        public UnityEvent eventToNext;
    }
    
    [SerializeField] private List<TextEvents> Texts;
    [SerializeField] private TMPro.TMP_Text text;
    [SerializeField] private WaveHandler WaveHandler;
    [SerializeField] private BugCreator BugCreator;
    [SerializeField] private GunSwitcher GunSwitcher;
    [SerializeField] private Transform InfrontOfPlayer;
    private int _currentIndex = 0;

    private void ShowSwitchedGun(GameObject gameObject)
    {
        if(_currentIndex == 0)
        {
            _currentIndex++;
            text.text = Texts[_currentIndex].textToShow; 
        }
    }
    private void ShowAmmospawn(GameObject gameObject)
    {
        if (_currentIndex == 1)
        {
            _currentIndex++;
            text.text = Texts[_currentIndex].textToShow;
        }
    }
    private void ShowOnDeath(int index)
    {
        if (_currentIndex == 2)
        {
            _currentIndex++;
            text.text = Texts[_currentIndex].textToShow;
            StartCoroutine(WaveHandler.CountdownToNextWave(15f));
            BugCreator.AmmoSpawnEvent -= ShowAmmospawn;
            WaveHandler.OnDeath -= ShowOnDeath;
            GunSwitcher.GunSwitch -= ShowSwitchedGun;
            Destroy(gameObject, 10f);
        }
    }
    private void Start()
    {
        text.text = Texts[_currentIndex].textToShow;
        BugCreator.AmmoSpawnEvent += ShowAmmospawn;
        WaveHandler.OnDeath += ShowOnDeath;
        GunSwitcher.GunSwitch += ShowSwitchedGun;
        WaveHandler.SpawnSpawner(1, InfrontOfPlayer.position, UnityEngine.Random.rotation);
    }
    private void OnDestroy()
    {
        if(text != null)
            text.gameObject.SetActive(false);
    }
}
