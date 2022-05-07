using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class JapanGirlHandler : MonoBehaviour
{
    [SerializeField] GameObject[] enemies;
    [SerializeField] float attackDist;
    [SerializeField] GameObject waypoint;
    [SerializeField] GameObject facing;
    [SerializeField] GameObject katana;
    [SerializeField] GameObject talker;
    [SerializeField] GameObject fighter;
    NavMeshAgent agent;
    Animator anim;
    int counter;
    bool cooldown;
    public bool swing;
    bool sheathing;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponentInParent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //print(Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position));
        if (Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) < 50)
        {
            Routine();
        }
    }

    void Routine()
    {
        if (enemies[0] != null)
        {
            if (Vector3.Distance(transform.position, enemies[0].transform.position) < attackDist && !cooldown)
                StartCoroutine(Attack());
            else
            {
                agent.isStopped = false;
                agent.SetDestination(enemies[0].transform.position);
                anim.SetBool("Walk", true);
            }
        }

        else if (enemies[0] == null && enemies[1] != null)
        {
            if (Vector3.Distance(transform.position, enemies[1].transform.position) < attackDist && !cooldown)
                StartCoroutine(Attack());
            else
            {
                agent.isStopped = false;
                agent.SetDestination(enemies[1].transform.position);
                anim.SetBool("Walk", true);
            }
        }

        else if (enemies[0] == null && enemies[1] == null && enemies[2] != null)
        {
            if (Vector3.Distance(transform.position, enemies[2].transform.position) < attackDist && !cooldown)
                StartCoroutine(Attack());
            else
            {
                agent.isStopped = false;
                agent.SetDestination(enemies[2].transform.position);
                anim.SetBool("Walk", true);
            }
        }

        else if (enemies[0] == null && enemies[1] == null && enemies[2] == null)
        {
            StartCoroutine(Sheath());
            agent.SetDestination(waypoint.transform.position);
            if (agent.remainingDistance < 0.1f)
            {
                talker.SetActive(true);
                talker.transform.LookAt(facing.transform.position);
                gameObject.SetActive(false);
            }
        }
    }

    IEnumerator Attack()
    {
        swing = true;
        anim.SetBool("Walk", false);
        cooldown = true;
        if (counter >= 3)
        {
            anim.SetTrigger("Special");
            counter = 0;
        }
        else
        {
            anim.SetTrigger("Attack");
            counter += 1;
        }

        yield return new WaitForSeconds(3f);
        cooldown = false;
    }

    IEnumerator Sheath()
    {
        anim.SetTrigger("Sheath");
        yield return new WaitForSeconds(0.2f);
        katana.SetActive(false);
    }
}
