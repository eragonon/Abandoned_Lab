using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float distance;
    public Transform Player;
    public UnityEngine.AI.NavMeshAgent navMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(this.transform.position , Player.position);

        if(distance < 10)
        {
            navMeshAgent.destination = Player.position;
        }
    }
}
