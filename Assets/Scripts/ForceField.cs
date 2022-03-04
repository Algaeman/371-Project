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

    void OnTriggerEnter(Collider other)
    {
     if ((other.tag == "forcefieldinitiator"))
        {
            
            forcefield.SetActive(true);
            forcefieldinitator.SetActive(false); 
            timeWhenDisappear = Time.time + timeToAppear;
        
        }

        if ((other.tag == "Bullet") && (forcefield.activeSelf) )
        {
            Destroy(other.gameObject);
        }
    
        }

    void OnCollisionEnter(Collision collision)
    {

        if ((collision.transform.tag == "Bullet") && (gameObject.tag == "Player") && (forcefield.activeSelf) )
        {
            Destroy(collision.gameObject);
        }
    }
}
