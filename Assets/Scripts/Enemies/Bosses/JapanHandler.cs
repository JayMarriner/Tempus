using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class JapanHandler : MonoBehaviour
{
    [Header("Controls")]
    [SerializeField] float lockDistance;
    [SerializeField] float attackDistance;
    [SerializeField] GameObject eyes;
    Animator anim;
    NavMeshAgent agent;
    Vector3 playerPos;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float playerDist = Vector3.Distance(transform.position,playerPos);

        if (playerDist < lockDistance)
        {
            if(playerDist < attackDistance)
            {
                StartCoroutine(Attack());
            }
            else
            {
                WalkToward();
            }
        }
    }

    IEnumerator Attack()
    {
        eyes.SetActive(true);
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(1.5f);
        eyes.SetActive(false);
    }

    void WalkToward()
    {
        agent.SetDestination(playerPos);
    }
}
