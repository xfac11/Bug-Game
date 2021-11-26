using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AmmoAttract : MonoBehaviour
{
    struct Ammo
    {
        public bool isMoving;
        public GameObject theAmmoObject;
    }
    [SerializeField] private Gun Weapon;
    [SerializeField] private float Speed = 5.0f;
    [SerializeField] private float Distance = 1.0f;
    [SerializeField] private float TimeToScaleDown = 1.0f;
    private List<Ammo> AmmoPack;

    private void Awake()
    {
        AmmoPack = new List<Ammo>();
    }
    private void OnEnable()
    {
        FindObjectOfType<BugObjHandler>().AmmoSpawnEvent += AddAmmoToAttract;
    }
    private void OnDisable()
    {
        FindObjectOfType<BugObjHandler>().AmmoSpawnEvent -= AddAmmoToAttract;
    }
    private void AddAmmoToAttract(GameObject ammoObject)
    {
        Ammo ammo = new Ammo
        {
            isMoving = true,
            theAmmoObject = ammoObject
        };
        foreach (var item in AmmoPack)
        {
            if(item.theAmmoObject.gameObject.GetInstanceID() == ammo.theAmmoObject.GetInstanceID())
            {
                return;
            }
        }
        AmmoPack.Add(ammo);
    }
    private void Update()
    {
        for (int i = 0; i < AmmoPack.Count; i++)
        {
            Ammo item = AmmoPack[i];
            if (item.isMoving)
            {
                Vector3 direction = transform.position - item.theAmmoObject.transform.position;
                direction.Normalize();
                item.theAmmoObject.transform.position += direction * Speed * Time.deltaTime;
            }
            if(Vector3.Distance(item.theAmmoObject.transform.position,transform.position) < Distance)
            {
                LTDescr lTDescr = LeanTween.scale(item.theAmmoObject, new Vector3(0, 0, 0), TimeToScaleDown);
                lTDescr.setOnComplete(() => 
                {
                    GiveAmmo(item.theAmmoObject.GetComponent<AmmoDesc>().AmmoCount);
                    AmmoPack.Remove(item); 
                });
                lTDescr.destroyOnComplete = true;
            }
        }
    }

    private void GiveAmmo(int Ammo)
    {
        Weapon.Ammo += Ammo;
    }
}
