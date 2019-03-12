using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hill : MonoBehaviour
{
    // Start is called before the first frame update

    private bool empthy = true;
    private float time = 0;
    public float scoreTime = 2;
    public bool enabled = false;
    private GameObject playerInside;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!empthy)
        {
            time = time + Time.deltaTime;
            if(time > scoreTime)
            {
                playerInside.GetComponent<CharacterControll>().addToScore(CharacterControll.SCORE_ON_HILL);
                disable();
            }
        }
        else
        {
            time = 0;
        }
    }

    public void disable()
    {
        time = 0;
        GetComponent<SpriteRenderer>().color = Color.grey;
        enabled = false;
        empthy = true;
        GameObject[] hills = GameObject.FindGameObjectsWithTag("Hill");
        GameObject hill;
        do
        {
            hill = hills[UnityEngine.Random.Range(0, hills.Length)];

        } while (hill == this.gameObject && hills.Length > 1);
        if (hill != null)
        {
            hill.GetComponent<Hill>().enable();

        }



    }
    public void enable()
    {
        time = 0;
        GetComponent<SpriteRenderer>().color = Color.red;
        enabled = true;
        empthy = true;


    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player"&&empthy && enabled)
        {
            empthy = false;
            playerInside = collision.gameObject;
            GetComponent<SpriteRenderer>().color = Color.green;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !empthy && enabled)
        {
            empthy = true;
            playerInside = null;
            GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

}
