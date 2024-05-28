using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class npc_controller : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject PATH;

    public Animator animator;

    public Transform[] PathPoints;
    public float minDistance = 10;
    public int index = 0;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        InitializePathPoints();
        if (PathPoints.Length > 0)
        {
            agent.SetDestination(PathPoints[index].position);
        }
    }

    private void Update()
    {
        roam();
    }

    void InitializePathPoints()
    {
        PathPoints = new Transform[PATH.transform.childCount];

        for (int i = 0; i < PathPoints.Length; i++)
        {
            PathPoints[i] = PATH.transform.GetChild(i);
        }
    }

    void roam()
    {
        if (PathPoints.Length == 0) return;

        float distance = Vector3.Distance(transform.position, PathPoints[index].position);
        

        if (distance < minDistance)
        {
            index = (index + 1) % PathPoints.Length;
            
        }

        agent.SetDestination(PathPoints[index].position);
    }
}
