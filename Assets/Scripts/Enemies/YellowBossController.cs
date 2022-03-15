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
    [SerializeField] ParticleSystem explosion;
    [SerializeField] GameObject forceField;
    public Shake cameraShake;

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
                cameraShake.start = true;
                StartHealthyBehavior();
                _initHealthyPhase = false;
            }
        }
        else if (health > 0.5 * _fullHealth)
        {
            if (_initWorriedPhase)
            {
                cameraShake.start = true;
                StartWorriedBehavior();
                _initWorriedPhase = false;
            }
        }
        else if (health > 0.25 * _fullHealth)
        {
            if (_initDesperatePhase)
            {
                cameraShake.start = true;
                StartDesperateBehavior();
                _initDesperatePhase = false;
            }
        }
        else
        {
            if (_initDyingPhase)
            {
                cameraShake.start = true;
                StartDyingBehavior();
                _initDyingPhase = false;
            }
        }
        
    }

    void StartHealthyBehavior()
    {
        // Play entry noise?  
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
        
        StartCoroutine(BecomeInvulnerable());
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
        rightMissileSpawner.spawnRate *= 0.8f;
        leftMissileSpawner.spawnRate -= 0.8f;

        StartCoroutine(BecomeInvulnerable());
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

    IEnumerator BecomeInvulnerable()
    {
        forceField.SetActive(true);
        yield return new WaitForSeconds(10);
        forceField.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            HandleDamageCollision(20, other.gameObject.transform.position);
        }
    }

    void HandleDamageCollision(int damage, Vector3 position)
    {
        health -= damage;
        //  Healthbar for Boss?  hb.setHealth(health);
        ParticleSystem ps = Instantiate(explosion, position, Quaternion.identity);
        ps.Play();
        StartCoroutine(waitForExplosion(ps));
    }

    private IEnumerator waitForExplosion(ParticleSystem ps)
    {
        yield return new WaitForSeconds(.25f);
        ps.Stop();
        if (health <= 0) Destroy(gameObject);
    }
}