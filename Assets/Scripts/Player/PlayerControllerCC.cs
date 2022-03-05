using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Based on Unity documentation for CharacterController.Move */

public class PlayerControllerCC : MonoBehaviour
{
    [SerializeField] float _speed = 5f;
    [SerializeField] float _jumpSpeed = 7f;
    [SerializeField] float _gravity = 2f;
    [SerializeField] float _gravityJumpModifier = 2f;
    [SerializeField] int maxHealth = 100;
    [SerializeField] string[] _powerUps; //contains the names of all possible powerups. Any new powerup should be added
    public int curHealth = 100;
    public HealthBar hb;
    public Vector3 wormholePos1;
    public Vector3 wormholePos2;
    public string powerUp = "none";

    CharacterController _characterController;
    Vector3 _moveDirection = Vector3.zero;
    float _gravityDownBoost = 2;

    public ParticleSystem explosion;


    void Start()
    {
        explosion.Stop();
        _characterController = GetComponent<CharacterController>();
        curHealth = maxHealth;
        hb.setMaxHealth(maxHealth);
    }

    void Update()
    {
        if (_characterController.isGrounded)
        {
            // We are grounded, so recalculate
            // move direction directly from axes
            _moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            _moveDirection *= _speed;

            // Face in the right direction
            if (_moveDirection != Vector3.zero)
            {
                transform.forward = _moveDirection;
            }

            // Handle jumping
            if (Input.GetButton("Jump"))
            {
                _moveDirection.y = _jumpSpeed;
            }
        }

        // Need to continually apply gravity to player
        _moveDirection.y -= _gravityJumpModifier * _gravity * Time.deltaTime;

        // If jumping, apply more gravity on the way down to make jump "feel" better
        if (_moveDirection.y < 0)
        {
            _moveDirection.y -= _gravityDownBoost * _gravityJumpModifier * _gravity * Time.deltaTime;
        }

        // Move the controller
        _characterController.Move(_moveDirection * Time.deltaTime);
    }

    public void takeDamage(int damage)
    {
        curHealth -= damage;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Missile"))
        {
            HandleDestructiveCollision(30);
        }

        if (other.CompareTag("BossBullet"))
        {
            HandleDestructiveCollision(20);
        }
        
        if (other.CompareTag("Bullet"))
        {
            Destroy(other);
            HandleDestructiveCollision(20);
        }

        if (other.CompareTag("Mine"))
        {
            Debug.Log(other.gameObject);
            Destroy(other.gameObject);
            HandleDestructiveCollision(50);
        }

        if (other.CompareTag("Wormhole1"))
        {
            StartCoroutine("TeleportToP2");
        }


        if (other.CompareTag("Wormhole2"))
        {
            StartCoroutine("TeleportToP1");
        }

        if (other.CompareTag("Missile"))
        {
            Debug.Log("Missile hit player.");
            takeDamage(10);
            hb.setHealth(curHealth);
            explosion.Play();
            StartCoroutine("waitForExplosion");
        }

        if(other.CompareTag("Powerup") && powerUp == "none")
        {
            Debug.Log("Pickup");
            powerUp = other.name;
            Destroy(other);
        }
    }

    void HandleDestructiveCollision(int damage)
    {
             takeDamage(damage);
             hb.setHealth(curHealth);
             explosion.Play();
             StartCoroutine("waitForExplosion");       
    }

    IEnumerator TeleportToP2()
    {
        _characterController.enabled = false;
        yield return new WaitForSeconds(0.1f);
        gameObject.transform.position = wormholePos2;
        yield return new WaitForSeconds(0.1f);
        _characterController.enabled = true;
    }

    IEnumerator TeleportToP1()
    {
        _characterController.enabled = false;
        yield return new WaitForSeconds(0.1f);
        gameObject.transform.position = wormholePos1;
        yield return new WaitForSeconds(0.1f);
        _characterController.enabled = true;
    }


    private IEnumerator waitForExplosion()
    {
        yield return new WaitForSeconds(.25f);
        explosion.Stop();
    }
}