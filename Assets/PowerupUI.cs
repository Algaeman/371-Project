using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerupUI : MonoBehaviour
{
    public GameObject minePowerup;
    public GameObject laserPowerup;

    public void showPowerup(string name)
    {
        Debug.Log("In showPowerup");
        if (name == "laser")
        {
            minePowerup.SetActive(false);
            laserPowerup.SetActive(true);
            Debug.Log("Laser powerup");
        }
        else if (name == "mine")
        {
            minePowerup.SetActive(true);
            laserPowerup.SetActive(false);
        }
    }    
    public void clearPowerup()
    {
        minePowerup.SetActive(false);
        laserPowerup.SetActive(false);
    }
}
