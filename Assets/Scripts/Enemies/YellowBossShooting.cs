using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBossShooting : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    public float bulletSpeed;
    public float shootingDelay;
    //[SerializeField] float burstAmount;

    float _shootingTimer;

    // Start is called before the first frame update
    void Start()
    {
        _shootingTimer = shootingDelay;
    }

    // Update is called once per frame
    void Update()
    {
        _shootingTimer -= Time.deltaTime;
        if (_shootingTimer <= 0)
        {
            _shootingTimer = shootingDelay;
            var bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;
        }
    }
}