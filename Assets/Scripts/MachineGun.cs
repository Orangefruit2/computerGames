using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MachineGun : MonoBehaviour
{
    public int bulletCount = 1;
    public float shootFrequenzy = 0.5f;
    public float reloadTime = 2;
    public Transform firePoint;
    public GameObject bullet;
    public float spray = 20;
    public int magazine = 3;
    public int ammunition = 5;
    private int currentMagazine;
    private int currentAmmunition;
    private float timer = 0;
    
    public enum WeaponState
    {
        READY,REALOAD,OUT_OF_AMMO
    };
    private WeaponState weaponState = WeaponState.READY;
    void Start()
    {
        currentAmmunition = ammunition;
        currentMagazine = magazine;
    }

    /**
     * Called if the player X press the fire button
   
     * */
    void TryToFire()
    {
        if(weaponState == WeaponState.READY)
        {
            if (timer >= shootFrequenzy)
            {
                timer = 0;
                for (int i = 0; i < bulletCount; i++)
                {
                    float rot = Random.Range(-spray, spray);
                    GameObject obj = Instantiate(bullet, firePoint.position, firePoint.rotation);
                    obj.SetActive(true);
                }
                setAmmunition(currentAmmunition - 1);
            }
        }
        else if(weaponState == WeaponState.REALOAD)
        {
            if(timer>= reloadTime)
            {
                setAmmunition(ammunition);
            }

        } else if(weaponState == WeaponState.OUT_OF_AMMO)
        {

        } else
        {
            throw new NotImplementedException();
        }

    }

    public void setAmmunition(int newAmmo)
    {
        if(newAmmo <= 0)
        {
            if (magazine == 0)
            {
                setState(WeaponState.OUT_OF_AMMO);
            } else
            {
                setState(WeaponState.REALOAD);
                setMagazine(currentMagazine - 1);
            }
        } else
        {
            setState(WeaponState.READY);
            currentAmmunition = newAmmo;
        }
       
       
    }

    private void setState(WeaponState state)
    {
        if (this.weaponState != state)
        {

            weaponState = state;
        }
    }

    public void setMagazine(int newmagazine)
    {
        if (newmagazine < 0) newmagazine = 0;
        if(weaponState==WeaponState.OUT_OF_AMMO && newmagazine > 0)
        {
            setState(WeaponState.REALOAD);
        }
        currentMagazine = newmagazine;

    }

    // Update is called once per frame
    void Update()
    {
       timer = timer + Time.deltaTime;
    }
}
