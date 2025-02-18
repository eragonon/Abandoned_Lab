using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform[] patrolPoints;
    public int targetPoint;
    public float speed;
    private GameObject player;
    private FieldOfView chase;
    private NavMeshAgent enemy;

    // Start is called before the first frame update
    void Start()
    {
        targetPoint = 0;
        chase = GetComponent<FieldOfView>();
        enemy = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Target");
    }

    // Update is called once per frame
    void Update()
    {
        if (chase.canSeePlayer)
        {
            Chase();
        } 
        else if (enemy.remainingDistance <= enemy.stoppingDistance)
        {
            increaseTargetInt();
        }
        transform.position = Vector3.MoveTowards(transform.position, patrolPoints[targetPoint].position, speed * Time.deltaTime);
    }

    void increaseTargetInt()
    {
        enemy.SetDestination(patrolPoints[targetPoint].position);
        
        if(targetPoint <= patrolPoints.Length)
        {
            targetPoint++;
            
        }
        if (targetPoint == 11)
        {
            targetPoint = 0;
        }
    }

    void Chase()
    {
        enemy.destination = player.transform.position;
    }
}

