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
        }
    }

    private void Execute(String equipped)
    {
        if (equipped == "none"){
            Debug.Log("No Powerup");
        }
        
    }
}
