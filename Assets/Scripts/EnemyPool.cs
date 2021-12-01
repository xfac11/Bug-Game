using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private int MaxEnemies;
    [SerializeField] private GameObject EnemyPrefab;
    private int currentIndex;
    private Queue<GameObject> InUse;
    private Queue<GameObject> AvailableEnemies;

    private void Awake()
    {
        InUse = new Queue<GameObject>();
        AvailableEnemies = new Queue<GameObject>();
        for (int i = 0; i < MaxEnemies; i++)
        {
            GameObject newObject = Instantiate(EnemyPrefab);
            newObject.SetActive(false);
            AvailableEnemies.Enqueue(newObject);
        }
    }

}
