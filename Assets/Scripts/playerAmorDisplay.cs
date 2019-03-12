using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAmorDisplay : MonoBehaviour
{
    public void updateWeaponState(MachineGun weapon)
    {
        if (weapon.getState() == MachineGun.WeaponState.OUT_OF_AMMO)
        {
            GetComponent<TextMesh>().text = "OUT OF AMMO";
        } else if(weapon.getState() == MachineGun.WeaponState.REALOAD)
        {
            GetComponent<TextMesh>().text = "RELOAD";
        } else
        {
            GetComponent<TextMesh>().text = weapon.getAmmo() + "/" + weapon.getMagazine();

        }
    }
}
