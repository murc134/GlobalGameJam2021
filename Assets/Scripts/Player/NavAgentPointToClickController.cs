using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavAgentPointToClickController : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent navagent;

    public NavMeshAgent NavAgent => navagent;

    private void Awake()
    {
        navagent = GetComponent<NavMeshAgent>();
    }
}
