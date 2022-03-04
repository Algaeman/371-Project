using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform playerTransform;
    public NavMeshAgent agent;
    private bool seen = false;
    public float followDistance = 3.0f;
    public float stopFollowDistance = 10.0f;
    public int curHealth = 100;
    private bool isColliding = false;

    float _nextShootTime;
    [SerializeField] AIBullet _bulletPrefab;
    //[SerializeField] Transform _shootPoint;
    [SerializeField] float _delay = 0.2f;
    [SerializeField] float _bulletSpeed = 5f;
    Vector3 direction;
    Vector3 bulletdirection;
    Queue<AIBullet> _pool = new Queue<AIBullet>();
    Vector3 offset = new Vector3(0, 0, 3);

    //Assumes:  1. GameObject tied to this script has a NavMeshAgent Component tied to agent
    //          2. The player's transform is tied to playerTransform
    //              - Player gameobject must be in a layer labeled "Player"
    //          3. A NavMesh has already been baked
    void Update()
    {
        if (!seen) {
           // Debug.Log("Seen");
            if (playerInView()) {
                seen = true;
               // Debug.Log("Seen = true");
            }
        }

        else {
            float distance = distanceToPlayer();
            if (!playerInView()) {
                //Debug.Log("Player in view");
                agent.isStopped = false;
                agent.SetDestination(playerTransform.position);
            }
            
            else if (distance < stopFollowDistance && distance >= followDistance) {
               // Debug.Log("Enemy in distance range");
                if (agent.isStopped == true)
                {
                   // Debug.Log("Agent is no longer stopped");
                    agent.isStopped = false;
                }
                agent.SetDestination(playerTransform.position);

            bulletdirection = (playerTransform.position - gameObject.transform.position).normalized;
            bulletdirection = new Vector3(bulletdirection.x, 0, bulletdirection.z);
            transform.forward = bulletdirection;
        //}
               
                if (CanShoot()) {
                   // Debug.Log("In here"); 
                    Shoot();
                   // ShootB();
                     }
            }
            
            else {
                agent.isStopped = true;
               // Debug.Log("Stopped");
                //Debug.Log(seen);
                //Debug.Log(playerInView());
                //Debug.Log(distance);
                //Shoot here
            }
        }
    }
    
    bool playerInView()
    {
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        Vector3 origin = transform.position;
        RaycastHit hit;
        if(Physics.Raycast(transform.position, direction, out hit, 30) && 
            hit.transform.gameObject.layer == LayerMask.NameToLayer("Player")) {
                return true;
        }
        return false;
    }

    float distanceToPlayer()
    {
        return Vector3.Distance(playerTransform.position, transform.position);
    }

    AIBullet GetBullet()
    {
        if (_pool.Count > 0)
        {
            var bullet = _pool.Dequeue();
            _bulletPrefab.gameObject.SetActive(true);
            return bullet;
        }
        else
        {   
        var bullet = Instantiate(_bulletPrefab, gameObject.transform.position + offset, gameObject.transform.rotation);
        return bullet;
        }
    }

    void Shoot()
    {
        _nextShootTime = Time.time + _delay;
        var bullet = GetBullet();
        bullet.transform.position = gameObject.transform.position;
        bullet.transform.rotation = gameObject.transform.rotation;
        bullet.GetComponent<Rigidbody>().velocity = bulletdirection * _bulletSpeed;
    }


    bool CanShoot()
    {
        return Time.time >= _nextShootTime;
    }

    public void AddToPool(AIBullet bullet)
    {
        _pool.Enqueue(bullet);
    }

//  void OnTriggerExit(Collider other)
//     {
//         if (other.CompareTag("PlayerBullet"))
//         {
//             Debug.Log(other);
//             curHealth -= 20;
//             Debug.Log(curHealth);
//             if(curHealth < 0){
//             Destroy(gameObject);
//             }
//         }
//     }


//     public void takeDamage(int damage){
//         curHealth -= damage;
//     }

void takeDamage(int damage){
    curHealth -= damage; 
}


//ALL ENEMY AI MUST HAVE KINEMATIC CHECKED 
void OnTriggerEnter (Collider other)
 {
     if(isColliding) return;
     isColliding = true;
     // Rest of the code
     if (other.CompareTag("PlayerBullet"))
        {
            Destroy(other);
            takeDamage(20);
            if(curHealth < 0){
                Destroy(gameObject);
            }
            

        }
     StartCoroutine(Reset());
 }

 IEnumerator Reset()
 {
      yield return new WaitForEndOfFrame();
      isColliding = false;
 }


}
