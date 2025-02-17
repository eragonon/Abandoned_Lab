using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float distance;
    public Transform Player;
    public UnityEngine.AI.NavMeshAgent navMeshAgent;
    public bool SetDestination(Vector3 target);
    public Transform[] patrolPoints;
    public int targetPoint;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        targetPoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position == patrolPoints[targetPoint].position)
        {
            increaseTargetInt();
        }
        transform.position = Vector3.MoveTowards(transform.position , patrolPoints[targetPoint].position, speed * Time.deltaTime);

        distance = Vector3.Distance(this.transform.position , Player.position);

        if(distance < 10)
        {
            UnityEngine.AI.NavMeshAgent.SetDestination = Player.position;
        }
    }

    void increaseTargetInt()
    {
        targetPoint++;

        if(targetPoint >= patrolPoints.Length)
        {
            targetPoint = 0;
        }
    }
}
