using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MallusHandler : MonoBehaviour
{
    [SerializeField] float attackDist;
    [SerializeField] float pauseTime;
    [SerializeField] float turnSpeed;
    int currAttack;
    bool attackCooldown;
    NavMeshAgent agent;
    Animator anim;
    Vector3 playerPos;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        agent.SetDestination(playerPos);
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        Routine();
    }

    void Routine()
    {
        if (!attackCooldown)
        {
            agent.SetDestination(playerPos);
            anim.SetBool("Walk", true);
        }
        else
        {
            agent.SetDestination(transform.position);
            anim.SetBool("Walk", false);
        }
        if(agent.remainingDistance < attackDist && !attackCooldown)
        {
            attackCooldown = true;
            print(currAttack);
            if (currAttack == 0)
                Attack1();
            else
                Attack2();
        }
    }

    void Attack1()
    {
        print("ys");
        anim.SetTrigger("Stomp");
        StartCoroutine(CoolDown());
        currAttack =1;
    }

    void Attack2()
    {
        print("ya2");
        anim.SetTrigger("Jump");
        StartCoroutine(CoolDown());
        currAttack = 0;
    }

    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(pauseTime);
        attackCooldown = false;
    }
}
