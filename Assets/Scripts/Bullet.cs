using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Weapon _gun;

    public void SetGun(Weapon gun) => _gun = gun;

    void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
        _gun.AddToPool(this);
    }
}