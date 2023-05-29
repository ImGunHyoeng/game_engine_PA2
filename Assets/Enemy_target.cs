using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_target : MonoBehaviour
{
    public GameObject target;
    NavMeshAgent agent;
    private void Start()
    {
        agent=GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        agent.destination=target.transform.position;
    }
}
