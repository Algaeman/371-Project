using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class ForceField : MonoBehaviour
{

    float startTime; 
    float elapsedTime; 
    float totalTime = 5f;
    float totalplayersTime; 
    public Slider forcefieldslider; 
    public GameObject forcefield;

    public void SetMaxForceFieldTime(float time)
    {
        forcefieldslider.maxValue = time;
        forcefieldslider.value = time;
    }

    public void SetForceFieldTime(float time)
    {
        forcefieldslider.value -= time ; 

    }

    void Start()
    {
       forcefield.SetActive(false); 
       SetMaxForceFieldTime(5); 
        
    }

    void Update()
    {

        if (Input.GetKeyDown("f"))
        {
            startTime = Time.time;
            
        }

        if (Input.GetKey("f"))
        { 
            Debug.Log("player's time so far " + totalplayersTime.ToString());
            Debug.Log("total time " + totalTime.ToString());

            if (totalplayersTime < totalTime)
            {
                forcefield.SetActive(true); 

            }

            if ((Time.time - startTime) > forcefieldslider.maxValue)
            {
                SetForceFieldTime(forcefieldslider.maxValue); 

            }
        }

        if (Input.GetKeyUp("f"))
        {
            elapsedTime = Time.time - startTime; 
            totalplayersTime = totalplayersTime + elapsedTime; 
            forcefield.SetActive(false); 
            SetForceFieldTime(elapsedTime); 
            
        }
    }
   
        
    void OnTriggerEnter(Collider other)
    {

        if ((other.tag == "Bullet") && (forcefield.activeSelf) )
        {
            Destroy(other.gameObject);
        }
        else if (other.tag == "forcefieldinitiator") 
        {
            Destroy(other.gameObject);
            forcefieldslider.value += 3; 
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
