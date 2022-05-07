using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBotHandler : EnemyHandler
{
    RobotInfo robotInfo;
    [SerializeField] bool isTargetingNpc;
    [SerializeField] GameObject npcTarget;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject shooter1;
    [SerializeField] GameObject shooter2;
    [SerializeField] float bulletSpeed;
    bool firingStarted;
    bool from1;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        robotInfo = GetComponent<RobotInfo>();
    }

    protected override void EnemyAction()
    {
        agent.isStopped = false;
        if (isTargetingNpc)
        {
            transform.LookAt(npcTarget.transform.position);
            if(!firingStarted)
               StartCoroutine(Shoot(npcTarget));
        }
        else
        {
            transform.LookAt(player.transform.position);
            if (!firingStarted)
                StartCoroutine(Shoot(player));
        }
    }

    IEnumerator Shoot(GameObject pos)
    {
        firingStarted = true;
        while (playerInArea)
        {
            shooter1.transform.LookAt(pos.transform.position);
            shooter2.transform.LookAt(pos.transform.position);
            GameObject newBullet = Instantiate(bullet);
            if (from1)
            {
                newBullet.transform.position = shooter1.transform.position;
                from1 = false;
            }
            else
            {
                newBullet.transform.position = shooter2.transform.position;
                from1 = true;
            }
            newBullet.GetComponent<Rigidbody>().AddForce(transform.forward * Time.deltaTime * bulletSpeed * 10);
            newBullet.transform.LookAt(pos.transform.position);
            yield return new WaitForSeconds(0.8f);
        }
        firingStarted = false;
    }
}
