using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateGuiPlayerStats : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void updateLive(float newLive)
    {
        if (newLive <= 0)
        {
            GetComponent<TextMesh>().text ="DEATH";

        } else
        {
            GetComponent<TextMesh>().text = (int)newLive + "%";

        }
    }

}
