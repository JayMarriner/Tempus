using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FutureBoss : MedBoss
{
    protected override void Attack()
    {
        if (playerDistance < attackDist)
        {
            nav.isStopped = true;
            isHitting = true;
            anim.SetTrigger("Attack");
        }
        else
        {
            if (nav.isStopped)
                nav.isStopped = false;
            isHitting = false;
            nav.SetDestination(player.transform.position);
        }
    }
}
