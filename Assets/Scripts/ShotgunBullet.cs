using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBullet : MonoBehaviour
{

    public int bulletCount = 4;
    public GameObject bullet;
    public Transform firePoint;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < bulletCount; i++)
        {
            GameObject obj = Instantiate(bullet, firePoint.position, firePoint.rotation);
            Instantiate(obj);
            obj.SetActive(true);
        }
    }
}
