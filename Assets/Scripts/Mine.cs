using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{

    public GameObject explosion;
    public GameObject instanexplosion;
    public PlayerControllerCC _playerControllerCC;
    // Start is called before the first frame update
    void Start()
    {
        //explosion.SetActive(false);
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
        _playerControllerCC.takeDamage(50);
        _playerControllerCC.hb.setHealth(_playerControllerCC.curHealth);
        instanexplosion = Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
        instanexplosion.SetActive(true);
        StartCoroutine("waitForExplosion");
        }
}

   private IEnumerator waitForExplosion(){
    //Debug.Log("here");
    yield return new WaitForSeconds(2f);
    instanexplosion.SetActive(false);
}
}
