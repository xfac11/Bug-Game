using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AmmoAttract : MonoBehaviour
{
    struct Ammo
    {
        public bool IsMoving;
        public GameObject TheAmmoObject;
        public AmmoDesc AmmoDesc;
    }
    [SerializeField] private Gun Weapon;
    [SerializeField] private float Speed = 5.0f;
    [SerializeField] private float Distance = 1.0f;
    [SerializeField] private float TimeToScaleDown = 1.0f;
    [SerializeField] private BugCreator BugCreator;
    private List<Ammo> AmmoPack;

    private void Awake()
    {
        AmmoPack = new List<Ammo>();
    }
    private void OnEnable()
    {
        BugCreator.AmmoSpawnEvent += AddAmmoToAttract;
    }
    private void OnDisable()
    {
        BugCreator.AmmoSpawnEvent -= AddAmmoToAttract;
    }
    private void AddAmmoToAttract(GameObject ammoObject)
    {
        Ammo ammo = new Ammo
        {
            IsMoving = true,
            TheAmmoObject = ammoObject,
            AmmoDesc = ammoObject.GetComponent<AmmoDesc>()
        };
        foreach (var item in AmmoPack)
        {
            if(item.TheAmmoObject.gameObject.GetInstanceID() == ammo.TheAmmoObject.GetInstanceID())
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
            if (item.IsMoving)
            {
                Vector3 direction = transform.position - item.TheAmmoObject.transform.position;
                direction.Normalize();
                item.TheAmmoObject.transform.position += direction * Speed * Time.deltaTime;
            }
            if(Vector3.Distance(item.TheAmmoObject.transform.position,transform.position) < Distance)
            {
                LTDescr lTDescr = LeanTween.scale(item.TheAmmoObject, new Vector3(0, 0, 0), TimeToScaleDown);
                lTDescr.setOnComplete(() => 
                {
                    GiveAmmo(item.AmmoDesc.AmmoCount);
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
