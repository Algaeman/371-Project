using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    public bool powerupSpawned = false;
    //Populate with points, each a different spawn location for the powerups (will be per level)
    public List<Vector3> points;
    // Start is called before the first frame update
    public GameObject immunityPowerup;
    public GameObject healthPowerup;
    public GameObject attackSpeedPowerup;
    public List<GameObject> powerups;
    private float time; 
    void Start()
    {
        time = Time.time;
        powerups.Add(immunityPowerup);
        powerups.Add(attackSpeedPowerup);
        powerups.Add(healthPowerup);        
    }

    // Update is called once per frame
    void Update()
    {
        if((Time.time - time) > 7.0f && powerupSpawned == false)
        {
            int pointIndex = Random.Range(0, points.Count - 1);
            int powerupIndex = Random.Range(0, powerups.Count - 1);
            powerupSpawned = true;
            Instantiate(powerups[powerupIndex], points[pointIndex], Quaternion.identity);
            time = Time.time;
        }
    }
}
