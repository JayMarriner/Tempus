using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FutureBoss : MedBoss
{
    [SerializeField] CinemachineVirtualCamera shoulderCam;
    [SerializeField] CinemachineFreeLook normCam;
    [SerializeField] ParticleSystem stompAttack;
    bool cooldown;

    protected override void Attack()
    {
        if (playerDistance < attackDist && !cooldown)
        {
            nav.isStopped = true;
            isHitting = true;
            anim.SetTrigger("Attack");
            GetComponent<CinemachineImpulseSource>().GenerateImpulse(1f);
            cooldown = true;
            StartCoroutine(Cooldown());
        }
        else if(playerDistance < attackDist && cooldown)
        {
            nav.isStopped = true;
        }
        else
        {
            if (nav.isStopped)
                nav.isStopped = false;
            isHitting = false;
            nav.SetDestination(player.transform.position);
        }
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(1.2f);
        stompAttack.Play();
        yield return new WaitForSeconds(10f);
        if(stompAttack.isPlaying)
            stompAttack.Stop();
        stompAttack.Clear();
        yield return new WaitForSeconds(0.1f);
        cooldown = false;
    }
}
