using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedRotation : MonoBehaviour
{

    public Quaternion rotation;
    void Awake()
    {
        rotation = transform.rotation;
    }
    void LateUpdate()
    {
        //Do not rotate, even if the player rotates
        //This can be used for the live of the player above the player
        transform.rotation = rotation;
    }
}
