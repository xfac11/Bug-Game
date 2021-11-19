using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugObjHandler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject Ammo;
    void Start()
    {
        foreach (var item in FindObjectsOfType<BuggedObject>())
        {
            item.Fixed += SpawnAmmo;
        }
    }

    private void SpawnAmmo(GameObject gameObject)
    {
        GameObject newAmmo = Instantiate(Ammo);
        newAmmo.transform.position = gameObject.transform.position;

    }
}
