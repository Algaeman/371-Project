using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineSpawner : MonoBehaviour
{

   [SerializeField] GameObject _mineprefab;
    [SerializeField] float _delay = 10f;

    public float _fireRate = 3f;
    private float _nextFire = 0f;

    // Update is called once per frame
    void Update()
    {
           Attack();         
    }

   void placeMine()
    {
        var mine = Instantiate(_mineprefab, gameObject.transform.position, gameObject.transform.rotation);
    }


  public void Attack()
    {
        if (Time.time > _nextFire)
        {
            _nextFire = Time.time + _fireRate;
             Debug.Log("In here"); 
            placeMine();
        }
    }
}
