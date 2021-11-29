using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestNavAgent : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    public Transform Target;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        _navMeshAgent.SetDestination(Target.position);
    }
}
