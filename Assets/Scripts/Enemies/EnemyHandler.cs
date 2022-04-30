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
    protected NavMeshAgent agent;
    protected GameObject player;
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
        if(player ==null)
            player = GameObject.FindGameObjectWithTag("Player");

        float playerDist = Vector3.Distance(gameObject.transform.position, player.transform.position);

        if (playerDist < lockOnDistance)
            playerInArea = true;

        if (playerDist < lockOnDistance && hasLockOn)
        {
            EnemyAction();
        }
        else if(playerDist > lockOnDistance || !hasLockOn)
        {
            agent.isStopped = false;
        }

        if (player.GetComponent<ThirdPersonPlayer>().specialHit)
            print(Vector3.Distance(player.transform.position, gameObject.transform.position));

        if (agent.remainingDistance < 0.01f)
            RandomWalk();
    }

    protected virtual void EnemyAction()
    {
        agent.isStopped = true;
        gameObject.transform.LookAt(player.transform);
        playerLock = true;
    }

    void RandomWalk()
    {
        agent.SetDestination(new Vector3(Random.Range(minPos.x, maxPos.x), gameObject.transform.position.y, Random.Range(minPos.z, maxPos.z)));
    }
}
