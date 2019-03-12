using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerNameDisplay : MonoBehaviour
{
    public void updatePlayerName(CharacterControll.Player player)
    {
        GetComponent<TextMesh>().text = player.ToString();
    }
}
