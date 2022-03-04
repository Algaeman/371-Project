using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBossController : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] YellowBossShooting farLeftTurret;
    [SerializeField] YellowBossShooting leftTurret;
    [SerializeField] YellowBossShooting farRightTurret;
    [SerializeField] YellowBossShooting rightTurret;
    [SerializeField] MissileSpawner rightMissileSpawner;
    [SerializeField] MissileSpawner leftMissileSpawner;

    float _fullHealth;

    bool _initHealthyPhase = true;
    bool _initWorriedPhase = true;
    bool _initDesperatePhase = true;
    bool _initDyingPhase = true;

    // Start is called before the first frame update
    void Start()
    {
        _fullHealth = health;
        
        // Make all weapons inactive
        farLeftTurret.gameObject.SetActive(false);
        leftTurret.gameObject.SetActive(false);
        farRightTurret.gameObject.SetActive(false);
        rightTurret.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        health -= Time.deltaTime;
        
        if (health > 0.75 * _fullHealth)
        {
            if (_initHealthyPhase)
            {
                StartHealthyBehavior();
                _initHealthyPhase = false;
            }
        }
        else if (health > 0.5 * _fullHealth)
        {
            if (_initWorriedPhase)
            {
                StartWorriedBehavior();
                _initWorriedPhase = false;
            }
        }
        else if (health > 0.25 * _fullHealth)
        {
            if (_initDesperatePhase)
            {
                StartDesperateBehavior();
                _initDesperatePhase = false;
            }
        }
        else
        {
            if (_initDyingPhase)
            {
                StartDyingBehavior();
                _initDyingPhase = false;

            }
        }
    }

    void StartHealthyBehavior()
    {
        // Play entry noise?  
        // Screen shake?
        farRightTurret.gameObject.SetActive(true);
        farLeftTurret.gameObject.SetActive(true);
    }

    void StartWorriedBehavior()
    {
        rightTurret.gameObject.SetActive(true);
        leftTurret.gameObject.SetActive(true);
        
        // Increase Missiles
        rightMissileSpawner.spawnNumber += 1;
        rightMissileSpawner.timeTracking += 2;
        leftMissileSpawner.spawnNumber += 1;
        leftMissileSpawner.timeTracking += 2;
    }

    void StartDesperateBehavior()
    {
        // Increase Bullets
        farRightTurret.bulletSpeed += 2;
        farRightTurret.shootingDelay *= 0.6f;
        rightTurret.bulletSpeed += 2;
        rightTurret.shootingDelay *= 0.6f;
        leftTurret.bulletSpeed += 2;
        leftTurret.shootingDelay *= 0.6f;
        farRightTurret.bulletSpeed += 2;
        farLeftTurret.shootingDelay *= 0.6f;
        
        // Increase Missiles
        rightMissileSpawner.spawnNumber += 1;
        rightMissileSpawner.timeTracking += 2;
        rightMissileSpawner.spawnRate *= 0.6f;
        leftMissileSpawner.spawnNumber += 1;
        leftMissileSpawner.timeTracking += 2;
        leftMissileSpawner.spawnRate -= 0.6f;
    }

    void StartDyingBehavior()
    {
        // Increase Bullets
        farRightTurret.bulletSpeed += 2;
        farRightTurret.shootingDelay *= 0.6f;
        rightTurret.bulletSpeed += 2;
        rightTurret.shootingDelay *= 0.6f;
        leftTurret.bulletSpeed += 2;
        leftTurret.shootingDelay *= 0.6f;
        farRightTurret.bulletSpeed += 2;
        farLeftTurret.shootingDelay *= 0.6f;
        
        // Increase Missiles
        rightMissileSpawner.spawnNumber += 1;
        rightMissileSpawner.timeTracking += 2;
        rightMissileSpawner.spawnRate *= 0.6f;
        leftMissileSpawner.spawnNumber += 1;
        leftMissileSpawner.timeTracking += 2;
     }
}