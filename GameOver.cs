using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    public int bullets_landed; 
    // Start is called before the first frame update
    void Start()
    {
        //gameoverPanel.SetActive(true); 
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bullets_landed >= 5)
        {
            SceneManager.LoadScene(0);
        }
            
    }

    void OnCollisionEnter(Collision collision)
    {

        Debug.Log("collision"); 

        //Debug.Log("COLLISION: " + collision.transform.tag + gameObject.tag); 
        
        // check the tage collision.tag TTE
        if (((collision.transform.tag == "BULLET") && (gameObject.tag == "Player")) || ((collision.transform.tag == "Enemy") && (gameObject.tag == "Player")))
        {
            //Debug.Log("COLLISION: " + collision.transform.name + gameObject.name); 
            bullets_landed ++;  
            //RestartGame.Instance.ResetScene1(); 
        }
    }



}
