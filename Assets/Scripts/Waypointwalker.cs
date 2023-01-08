using UnityEngine;
using UnityEngine.AI;

public class Waypointwalker : MonoBehaviour
{

    public Transform goal;

    void Start()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        goal = GameObject.Find("End").transform;
        agent.destination = goal.position;
    }
}