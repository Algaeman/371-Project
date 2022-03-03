using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSpawner : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] GameObject missilePrefab;
    [SerializeField] float timeTracking;
    [SerializeField] float spawnRate;
    [SerializeField] Transform spawnPoint;
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("launchMissle", spawnRate, spawnRate);
        
    }

    void launchMissle()
    {
        GameObject missile = Instantiate(missilePrefab, spawnPoint.position, Quaternion.identity);
        GuidedMissle guidedMissle = missile.GetComponent<GuidedMissle>();
        guidedMissle.timeTracking = timeTracking;
        guidedMissle.target = target;
    }
}
