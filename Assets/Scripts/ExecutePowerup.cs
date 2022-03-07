using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecutePowerup : MonoBehaviour
{
    PlayerControllerCC Player;

    void Start()
    {
        Player = FindObjectOfType<PlayerControllerCC>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            Execute(Player.powerUp);
            Player.powerUp = null;
        }
    }

    private void Execute(String equipped)
    {
        if (equipped == null){
            Debug.Log("No Powerup");
        }
        else if (equipped == "LaserPowerup")
        {
            Debug.Log("laser used");
            Player.laser.gameObject.SetActive(true);
            Player.laserAudio.gameObject.SetActive(true);
            Player.laserActivated = true;
        }
        
    }
}
