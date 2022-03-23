using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;

    public bool MoveBack;

    public NavMeshAgent enemyObj;


    private void Update()
    {
        if (MoveBack == true)
        {
            enemyObj.SetDestination(pointA.position);
            if (!enemyObj.pathPending)
            {
                if (enemyObj.remainingDistance <= enemyObj.stoppingDistance)
                {
                    enemyObj.SetDestination(pointB.position);
                    MoveBack = false;
                }
            }
        }
        else
        {
            enemyObj.SetDestination(pointB.position);
            if (!enemyObj.pathPending)
            {
                if (enemyObj.remainingDistance <= enemyObj.stoppingDistance)
                {
                    MoveBack = true;
                }
            }
        }
    }
}
