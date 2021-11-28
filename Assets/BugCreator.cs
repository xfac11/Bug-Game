using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugCreator : MonoBehaviour
{
    [SerializeField] private GameObject BugObjectPrefab;
    [SerializeField] private Transform PossiblePositions;
    [SerializeField] private float MinDistance = 1.0f;
    [SerializeField] private int MinimumBuggedObjects = 0;
    [SerializeField] private int MaximumBuggedObjects = 1;
    [SerializeField] private GameObject Ammo;
    [SerializeField] private float CheckTime = 1.0f;
    [SerializeField] private float SpawnTime = 1.0f;
    
    
    public Action<GameObject> AmmoSpawnEvent;
    private Transform[] _possiblePositionsArray;
    private List<int> _usedPositions;
    private int _currentBuggedObjects;
    private void Awake()
    {
        _currentBuggedObjects = 0;
        _possiblePositionsArray = PossiblePositions.GetComponentsInChildren<Transform>();
        _usedPositions = new List<int>();
        for (int i = 0; i < _possiblePositionsArray.Length; i++)
        {
            _usedPositions.Add(i);
        }
        
        
        StartCoroutine(StartSpawning());
    }
    private IEnumerator StartSpawning()
    {
        while(true)
        {
            if(_currentBuggedObjects < MaximumBuggedObjects || _currentBuggedObjects < MinimumBuggedObjects)
            {
                int nrOfObjectsToSpawn = MinimumBuggedObjects - _currentBuggedObjects;
                if(nrOfObjectsToSpawn <= 0)
                {
                    //Spawn only one
                    GameObject newObject = Instantiate(BugObjectPrefab);
                    newObject.GetComponent<BuggedObject>().Fixed += SpawnAmmo;
                    newObject.GetComponent<BuggedObject>().Fixed += DestoryBuggedObject;
                    newObject.transform.position = GetRandomPosition();
                    newObject.transform.rotation = UnityEngine.Random.rotation;
                    _currentBuggedObjects++;
                }
                else
                {
                    //Spawn as many as nrOfObjectsToSpawn is.
                    for (int i = 0; i < nrOfObjectsToSpawn; i++)
                    {
                        GameObject newObject = Instantiate(BugObjectPrefab);
                        newObject.GetComponent<BuggedObject>().Fixed += SpawnAmmo;
                        newObject.GetComponent<BuggedObject>().Fixed += DestoryBuggedObject;
                        newObject.transform.position = GetRandomPosition();
                        newObject.transform.rotation = UnityEngine.Random.rotation;
                        _currentBuggedObjects++;
                    }
                }
                yield return new WaitForSeconds(SpawnTime);
            }

            yield return new WaitForSeconds(CheckTime);
        }
    }

    private Vector3 GetRandomPosition()
    {
        int randomIndex = UnityEngine.Random.Range(0, _usedPositions.Count);
        int placesToSpawnIndex = _usedPositions[randomIndex];
        _usedPositions.RemoveAt(randomIndex);
        return _possiblePositionsArray[placesToSpawnIndex].position;
    }

    private void SpawnAmmo(GameObject gameObject)
    {
        GameObject newAmmo = Instantiate(Ammo);
        newAmmo.transform.position = gameObject.transform.position;
        AmmoSpawnEvent?.Invoke(newAmmo);
    }

    private void DestoryBuggedObject(GameObject gameObject)
    {
        for (int i = 0; i < _possiblePositionsArray.Length; i++)
        {
            if(_possiblePositionsArray[i].position == gameObject.transform.position)
            {
                _usedPositions.Add(i);
                break;
            }
        }
        LeanTween.scale(gameObject, new Vector3(0, 0, 0), 0.3f);
        Destroy(gameObject, 0.5f);
        _currentBuggedObjects--;
        if (_currentBuggedObjects < 0)
            _currentBuggedObjects = 0;
    }


}
