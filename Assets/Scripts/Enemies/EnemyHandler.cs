using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHandler : MonoBehaviour
{
    [Range(10, 100)]
    [SerializeField] int dist;
    [Range(10, 500)]
    [SerializeField] int lockOnDistance;
    [SerializeField] bool hasLockOn;
    Vector3 minPos;
    Vector3 maxPos;
    NavMeshAgent agent;
    GameObject player;
    bool playerLock;
    public bool playerInArea;

    private void Start()
    {
        minPos = new Vector3(gameObject.transform.position.x - dist, gameObject.transform.position.y, gameObject.transform.position.z - dist);
        maxPos = new Vector3(gameObject.transform.position.x + dist, gameObject.transform.position.y, gameObject.transform.position.z + dist);
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        RandomWalk();
    }

    private void Update()
    {
        float playerDist = Vector3.Distance(gameObject.transform.position, player.transform.position);

        if (playerDist < lockOnDistance)
            playerInArea = true;

        if (playerDist < lockOnDistance && hasLockOn)
        {
            agent.isStopped = true;
            gameObject.transform.LookAt(player.transform);
            playerLock = true;
        }
        else if(playerDist > lockOnDistance || !hasLockOn)
        {
            agent.isStopped = false;
        }


        if (agent.remainingDistance < 0.01f)
            RandomWalk();
    }

    void RandomWalk()
    {
        agent.SetDestination(new Vector3(Random.Range(minPos.x, maxPos.x), gameObject.transform.position.y, Random.Range(minPos.z, maxPos.z)));
    }
}
