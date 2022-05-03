using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MedBoss : MonoBehaviour
{
    [Range(1,100)]
    [SerializeField] int lockOnDist;
    [Range(1,50)]
    [SerializeField] int attackDist;
    [SerializeField] Animator anim;
    [Header("UI Stuff")]
    [SerializeField] GameObject canvasBoss;
    [SerializeField] Image fillImage;
    [SerializeField] GameObject key;
    ThirdPersonPlayer player;
    NavMeshAgent nav;
    public bool stopMovement;
    float playerDistance;
    bool filled;
    public bool isHitting;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonPlayer>();
        canvasBoss.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (stopMovement)
        {
            if (!key.activeSelf)
                key.SetActive(true);
            return;
        }
        playerDistance = Vector3.Distance(player.transform.position, gameObject.transform.position);
        if (playerDistance < lockOnDist)
        {
            Attack();
            canvasBoss.SetActive(true);
            if(!filled)
                StartCoroutine(FillBar());
        }
        else
        {
            filled = false;
            canvasBoss.SetActive(false);
        }

        anim.SetBool("Walk", !nav.isStopped);
    }

    void Attack()
    {
        if(playerDistance < attackDist)
        {
            gameObject.transform.LookAt(player.transform);
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

    IEnumerator FillBar()
    {
        filled = true;
        fillImage.fillAmount = 0;
        while (fillImage.fillAmount < 1)
        {
            fillImage.fillAmount += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
