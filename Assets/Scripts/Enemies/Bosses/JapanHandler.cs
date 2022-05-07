using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class JapanHandler : MedBoss
{
    [Header("Controls")]
    [SerializeField] GameObject eyes;
    [SerializeField] GameObject RockRain;

    bool attackCool;
    bool firstPath;
    int attackNum;

    protected override void Attack()
    {
        //playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        //float playerDist = Vector3.Distance(transform.position,playerPos);
        if (!attackCool  && playerDistance < attackDist)
        {
            attackCool = true;
            if (attackNum == 0)
            {
                StartCoroutine(Attack1());
            }
            else
            {
                StartCoroutine(RainAttack());
            }
        }
        if (playerDistance < lockOnDist)
            WalkToward();
    }

    IEnumerator Attack1()
    {
        attackNum = 1;
        print("yes1");
        eyes.SetActive(true);
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(3f);
        eyes.SetActive(false);
        yield return new WaitForSeconds(3f);
        attackCool = false;
    }

    IEnumerator RainAttack()
    {
        attackNum = 0;
        print("yes2");
        anim.SetTrigger("RainAttack");
        yield return new WaitForSeconds(0.5f);
        GameObject newRock = Instantiate(RockRain);
        newRock.transform.position = player.transform.position;
        yield return new WaitForSeconds(3f);
        attackCool = false;
    }

    void WalkToward()
    {
        if (nav.remainingDistance < 1f && firstPath)
        {
            nav.SetDestination(gameObject.transform.position);
            anim.SetBool("Walk", false);
        }
        else
        {
            anim.SetBool("Walk", true);
            nav.SetDestination(GameObject.FindGameObjectWithTag("Player").transform.position);
        }
    }
}
