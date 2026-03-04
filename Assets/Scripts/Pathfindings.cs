using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.AI;

public class Pathfindings : MonoBehaviour
{
    [SerializeField] private Transform[] objectifs;
    private NavMeshAgent agent;
    private Vector3 currentDestination;
    private int currentDestinationIndex = 0;
    void Start()
    {
        currentDestination = objectifs[0].position;
        agent = GetComponent<NavMeshAgent>();
        if (agent != null && objectifs != null)
        {
            agent.SetDestination(currentDestination);
        }
    }
    
    void Update()
    {
        if (Vector3.Distance(transform.position, currentDestination) < 1f)
        {
            currentDestinationIndex += 1;
            currentDestination = objectifs[currentDestinationIndex % objectifs.Length].position;
            agent.SetDestination(currentDestination);
        }
    }
}

