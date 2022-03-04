using UnityEngine;
using UnityEngine.AI;

public class MissileShipController : MonoBehaviour
{
    [SerializeField] GameObject target;
    
    NavMeshAgent _navMeshAgent;
    
    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        _navMeshAgent.SetDestination(target.transform.position);

    }
}
