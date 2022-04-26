using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBotHandler : EnemyHandler
{
    [Range(1,5)]
    [SerializeField] int explodeDistance;
    [Range(1,5)]
    [SerializeField] int explodeRadius;
    [Range(0, 1)]
    [SerializeField] float explodeTime;
    [SerializeField] GameObject halo;
    [SerializeField] GameObject enemyModel;
    ParticleSystem particle;
    bool started;
    protected override void EnemyAction()
    {
        agent.SetDestination(player.transform.position);
        print(Vector3.Distance(transform.position, player.transform.position));
        if (Vector3.Distance(transform.position, player.transform.position) < explodeDistance)
        {
            if(!started)
                StartCoroutine(Explode());
        }
    }

    IEnumerator Explode()
    {
        started = false;
        agent.isStopped = true;
        yield return new WaitForSeconds(0.2f);
        for(int x = 0; x < 2; x++)
        {
            if (Vector3.Distance(transform.position, player.transform.position) > explodeRadius)
            {
                agent.isStopped = false;
                started = false;
                halo.SetActive(false);
                yield break;
            }
            halo.SetActive(!halo.activeSelf);
            yield return new WaitForSeconds(explodeTime);
        }
        halo.SetActive(false);
        enemyModel.SetActive(false);
        particle = GetComponentInChildren<ParticleSystem>();
        particle.Play();
        yield return new WaitForSeconds(0.6f);
        Destroy(gameObject);
        player.GetComponent<ThirdPersonPlayer>().TakeDamage(2.5f);

    }
}
