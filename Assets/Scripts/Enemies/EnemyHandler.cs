using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHandler : MonoBehaviour
{
    [Range(10, 100)]
    [SerializeField] int dist;
    Vector3 minPos;
    Vector3 maxPos;
    NavMeshAgent agent;

    private void Start()
    {
        minPos = new Vector3(gameObject.transform.position.x - dist, gameObject.transform.position.y, gameObject.transform.position.z - dist);
        maxPos = new Vector3(gameObject.transform.position.x + dist, gameObject.transform.position.y, gameObject.transform.position.z + dist);
        agent = GetComponent<NavMeshAgent>();
        RandomWalk();
    }

    private void Update()
    {
        if (agent.remainingDistance < 0.01f)
            RandomWalk();
    }

    void RandomWalk()
    {
        print("ping");
        agent.SetDestination(new Vector3(Random.Range(minPos.x, maxPos.x), gameObject.transform.position.y, Random.Range(minPos.z, maxPos.z)));
    }
}
