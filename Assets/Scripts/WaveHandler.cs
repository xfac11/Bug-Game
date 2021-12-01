using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveHandler : MonoBehaviour
{
    [Serializable]struct SpawnerLocation
    {
        public Transform Transform;
        public bool Used;
    }
    /// <summary>
    /// Wavenumber, Difficulty
    /// </summary>
    public Action<int,int> OnNewWave;
    public Action OnWaveFinished;
    public Action<int> OnDeath;
    [SerializeField] private GameObject SpawnerPrefab;
    [SerializeField] private int Difficulty;
    [SerializeField] private int WaveNumber;
    [SerializeField] private int TimeBetweenWaves;
    [SerializeField] private List<AIEnemy.EnemyStats> EnemyStats;
    [SerializeField] private Transform PlacesToSpawnParent;
    [SerializeField] private int Randomize;
    private List<SpawnerLocation> PlacesToSpawn;
    private List<int> _availableLocations;
    private int _numberOfEnemies = 0;
    private int _numberOfSpawners = 0;
    private int _currentEnemies = 0;
    private int _currentSpawners = 0;
    private int _timer;
    private AudioSource _newWaveSound;
    /// <summary>
    /// Returns the timer and incremented by 1 to account for float
    /// </summary>
    public int Timer
    {
        get
        {
            return _timer;
        }
    }
    private void Awake()
    {
        _newWaveSound = GetComponent<AudioSource>();
        _availableLocations = new List<int>();
        PlacesToSpawn = new List<SpawnerLocation>();
        foreach (var item in PlacesToSpawnParent.GetComponentsInChildren<Transform>())
        {
            SpawnerLocation spawnerLocation = new SpawnerLocation
            {
                Transform = item,
                Used = false
            };
            PlacesToSpawn.Add(spawnerLocation);
        }
        for (int i = 0; i < PlacesToSpawn.Count; i++)
        {
            _availableLocations.Add(i);
        }
    }

    private void OnEnable()
    {
        AIEnemy.OnDeath += EnemyDied;
    }
    private void OnDisable()
    {
        AIEnemy.OnDeath -= EnemyDied;
    }

    private void EnemyDied()
    {
        _currentEnemies--;
        OnDeath?.Invoke(_currentEnemies);//To update the UI. If there are 10 enemies left the UI will display that.
        if(_currentEnemies <= 0)
        {
            _currentEnemies = 0;
            StartCoroutine(CountdownToNextWave());
            OnWaveFinished?.Invoke();
        }
    }
    public IEnumerator CountdownToNextWave(float countdownTime)
    {
        float time = countdownTime;
        while (time > 0)
        {
            time -= Time.deltaTime;
            _timer = (int)time;
            yield return null;
        }
        StartWave();
    }

    private IEnumerator CountdownToNextWave()
    {
        float time = TimeBetweenWaves;
        while(time > 0)
        {
            time -= Time.deltaTime;
            _timer = (int)time;
            yield return null;
        }
        StartWave();
    }

    private void StartWave()
    {
        ResetSpawnerLocations();
        if(WaveNumber == 7)
        {
            WaveNumber = 0;
            Difficulty++;
        }
        WaveNumber++;
        int spawners = Mathf.Min((int)(Mathf.Pow(WaveNumber, 2) * 0.2f) + 1, 5);
        int enemies = Mathf.Min((int)(Mathf.Pow(spawners, 0.5f) * 2) + 1, 10);

        _numberOfEnemies = enemies;
        _numberOfSpawners = spawners;

        OnNewWave?.Invoke(WaveNumber,Difficulty);

        StartCoroutine(StartSpawning());
    }

    private void ResetSpawnerLocations()
    {
        _availableLocations = new List<int>();
        for (int i = 0; i < PlacesToSpawn.Count; i++)
        {
            _availableLocations.Add(i);
        }
    }

    IEnumerator StartSpawning()
    {
        _currentEnemies = _numberOfEnemies * _numberOfSpawners;
        while(_numberOfSpawners != 0)
        {
            SpawnSpawner();
            _numberOfSpawners--;
            if (_numberOfSpawners < 0)
                _numberOfSpawners = 0;
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }

    private void SpawnSpawner()
    {
        GameObject newSpawnerObj = Instantiate(SpawnerPrefab);
        Spawner spawner = newSpawnerObj.GetComponent<Spawner>();
        PrepareSpawner(spawner, _numberOfEnemies, GetRandomPosition(), UnityEngine.Random.rotation);
    }
    public void SpawnSpawner(int enemiesToSpawn, Vector3 position, Quaternion rotation)
    {
        GameObject newSpawnerObj = Instantiate(SpawnerPrefab);
        Spawner spawner = newSpawnerObj.GetComponent<Spawner>();
        PrepareSpawner(spawner, enemiesToSpawn, position, rotation);
    }
    private void PrepareSpawner(Spawner spawner, int enemiesToSpawn, Vector3 position, Quaternion rotation)
    {
        spawner.EnemyStats = GetStats();
        spawner.SetEnemiesToSpawn(enemiesToSpawn);
        spawner.SetSpawnRate(4.5f);
        spawner.transform.position = position;
        spawner.transform.eulerAngles = new Vector3(0f, rotation.eulerAngles.y, 0f);
        spawner.OnFinish += SpawnerFinished;
    }

    private AIEnemy.EnemyStats GetStats()
    {
        if (Difficulty < EnemyStats.Count)
        {
            return EnemyStats[Difficulty];
        }
        else
        {
            return EnemyStats[EnemyStats.Count - 1].IncreasePositive(speedPer:1.2f,damageRangePer:1.0f,attackRatePer:0.95f,healthPer:1.1f,damagePer:1.3f);
        }
    }

    private void SpawnerFinished(GameObject obj)
    {
        Destroy(obj,1f);//Destroys the spawner object
    }

    private Vector3 GetRandomPosition()
    {
        int randomIndex = UnityEngine.Random.Range(0, _availableLocations.Count);
        int placesToSpawnIndex = _availableLocations[randomIndex];
        _availableLocations.RemoveAt(randomIndex);
        PlacesToSpawn[placesToSpawnIndex] = new SpawnerLocation { Transform = PlacesToSpawn[placesToSpawnIndex].Transform, Used = true };

        return PlacesToSpawn[placesToSpawnIndex].Transform.position;
    }
}
