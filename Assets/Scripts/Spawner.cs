using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject Enemy;
    [SerializeField] private int EnemiesToSpawn;
    /// <summary>
    /// How often to spawn enemies. If 0 it will spawn all at once.
    /// </summary>
    [SerializeField] private float SpawnRate;
    [SerializeField] private GameObject SpawnerEffect;
    public bool DestroyOnFinish = false;
    public void SetEnemiesToSpawn(int enemiesToSpawn)
    {
        EnemiesToSpawn = enemiesToSpawn;
    }
    public void AddEnemiesToSpawn(int enemiesToAdd)
    {
        EnemiesToSpawn += enemiesToAdd;
    }
    public void SetSpawnRate(float rate)
    {
        SpawnRate = rate;
    }
    IEnumerator SpawnEnemies()
    {
        while(EnemiesToSpawn > 0)
        {
            GameObject spawnedEnemy = Instantiate(Enemy);
            spawnedEnemy.transform.position = new Vector3(transform.position.x, 1.167f, transform.position.z);//TODO: Fix position in y for the bugs. Maybe some type of gravity instead of hardcoded
            EnemiesToSpawn--;
            yield return new WaitForSeconds(SpawnRate);
        }
        if(DestroyOnFinish)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }
}
