using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MedBoss : MonoBehaviour
{
    [Range(1,100)]
    [SerializeField] int lockOnDist;
    [Range(1,50)]
    [SerializeField] int attackDist;
    [SerializeField] Animator anim;
    ThirdPersonPlayer player;
    NavMeshAgent nav;
    float playerDistance;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        playerDistance = Vector3.Distance(player.transform.position, gameObject.transform.position);
        if (playerDistance < lockOnDist)
        {
            Attack();
        }

        anim.SetBool("Walk", !nav.isStopped);
    }

    void Attack()
    {
        if(playerDistance < attackDist)
        {
            gameObject.transform.LookAt(player.transform);
            nav.isStopped = true;
            anim.SetTrigger("Attack");
        }
        else
        {
            if (nav.isStopped)
                nav.isStopped = false;
            nav.SetDestination(player.transform.position);
        }
    }
}
