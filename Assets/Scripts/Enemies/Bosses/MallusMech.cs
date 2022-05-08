using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MallusMech : MedBoss
{
    bool cooled;
    int currAttack;
    [SerializeField] float waitTime;
    [SerializeField] GameObject RockRain;
    [SerializeField] ParticleSystem stompAttack;

    protected override void Attack()
    {
        if (playerDistance < attackDist)
        {
            if (!cooled)
            {
                transform.LookAt(player.transform.position);
                cooled = true;
                if (currAttack == 0)
                {
                    currAttack = 1;
                    anim.SetTrigger("Stomp");
                    StartCoroutine(Cooldown());
                    StartCoroutine(Stomp());
                }
                else
                {
                    currAttack = 0;
                    anim.SetTrigger("Jump");
                    StartCoroutine(Cooldown());
                    StartCoroutine(Rain());
                }
            }
        }
        else
        {
            nav.SetDestination(player.transform.position);
        }
    }

    IEnumerator Rain()
    {
        yield return new WaitForSeconds(1f);
        GameObject newRock = Instantiate(RockRain);
        newRock.transform.position = player.transform.position;
    }

    IEnumerator Stomp()
    {
        yield return new WaitForSeconds(1.2f);
        stompAttack.Play();
        yield return new WaitForSeconds(10f);
        if (stompAttack.isPlaying)
            stompAttack.Stop();
        stompAttack.Clear();
        yield return new WaitForSeconds(0.1f);
    }

    IEnumerator Cooldown()
    {
        mallusStopper = true;
        yield return new WaitForSeconds(waitTime / 2);
        mallusStopper = false;
        yield return new WaitForSeconds(waitTime/2);
        //anim.SetBool("Walk", false);
        cooled = false;
    }
}
