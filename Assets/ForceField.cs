using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour
{


    public GameObject forcefield;
    public GameObject forcefieldinitator;
    private float timeToAppear = 5f;
    private float timeWhenDisappear;
    
    // Start is called before the first frame update
    void Start()
    {
       forcefield.SetActive(false); 
       forcefieldinitator.SetActive(true); 
        
    }

    // Update is called once per frame
    void Update()
    {
        if ( forcefield.activeSelf && (Time.time >= timeWhenDisappear))
        {
            forcefield.SetActive(false); 
        }
        
    }

    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Collision"); 
        //Debug.Log("COLLISION: " + collision.transform.tag + gameObject.tag); 
        
        if ((collision.transform.tag == "forcefieldinitiator") && (gameObject.tag == "Player"))
        {
            forcefield.SetActive(true);
            forcefieldinitator.SetActive(false); 
            timeWhenDisappear = Time.time + timeToAppear;
        
        }

        if ((collision.transform.tag == "Bullet") && (gameObject.tag == "Player") && (forcefield.activeSelf) )
        {
            Destroy(collision.gameObject);
        }
    }
}

