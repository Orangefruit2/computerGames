using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Item : MonoBehaviour
{
    private Collider2D m_Collider;
    public bool activ = true;
    public double hi;
    private Renderer rend;
    private Transform rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Transform>();
        m_Collider = GetComponent<Collider2D>();
        rend = GetComponent<Renderer>();
        if (activ == false)
        {
            spawnItem();
        }
    }

    void Update()
    {
        float chance = UnityEngine.Random.Range(0, 100);
        if (chance > 97 && activ==false)
        {
            spawnItem();

        }
    }

    void spawnItem()
    {
        if (activ == false)
        {
            //gameObject.SetActive(true);
            translateItem();
            Debug.Log("I am here !");
            m_Collider.enabled = !m_Collider.enabled;
            rend.enabled = !rend.enabled;
            activ = true;
        }
    }

    void translateItem()
    {
        float chance = UnityEngine.Random.Range(0, 100);
        Vector2 newPosition1 = new Vector2(-4, 0);
        Vector2 newPosition2 = new Vector2(0, 4);
        Vector2 newPosition3 = new Vector2(4, 0);
        Vector2 newPosition4 = new Vector2(0,-4);

        if(chance<25 && rigidBody.position.x > -10) rigidBody.Translate(newPosition1);
        if (chance >= 25 && chance < 50 && rigidBody.position.y < 10) rigidBody.Translate(newPosition2);
        if (chance >= 50 && chance < 75 && rigidBody.position.x < 10) rigidBody.Translate(newPosition3);
        if (chance >= 75 && rigidBody.position.y > -10) rigidBody.Translate(newPosition4);
    }

    void despawnItem()
    {
        
        if (activ == true)
        {
            //gameObject.SetActive(false);
            m_Collider.enabled = !m_Collider.enabled;
            rend.enabled = !rend.enabled;
            activ = false;
        }
    }
}
