using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotInfo : MonoBehaviour
{
    [SerializeField] float health = 100;
    [SerializeField] Image healthImg;
    [SerializeField] bool isDummy;
    [SerializeField] bool isMallus;
    [SerializeField] bool isBoss;
    [SerializeField] GameObject door;
    [SerializeField] GameObject[] otherTargets;
    public float GetHealth { get => health; }
    public Shooter shooterScript;
    ThirdPersonPlayer player;
    bool bossIsDying;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonPlayer>();
        if (isMallus)
        {
            health = 150;
        }
    }
    private void Update()
    {
        
    }

    public void LowerHealth(int amt)
    {
        health -= amt;
        if (!isMallus)
            healthImg.fillAmount = health / 100;
        else
            healthImg.fillAmount = health / 150;
        if (isBoss)
        {
            GetComponentInChildren<Animator>().SetTrigger("Hit");
        }
        if (health <= 0)
        {
            if (isBoss)
            {
                GetComponent<MedBoss>().stopMovement = true;
                if(!bossIsDying)
                    StartCoroutine(BossDeath());
                return;
            }
            if (isDummy || isMallus)
            {
                door.SetActive(true);
            }

            if (isMallus)
            {
                for(int x  = 0; x < otherTargets.Length; x++)
                {
                    Destroy(otherTargets[x]);
                }
            }

            Destroy(gameObject);
        }
    }

    IEnumerator BossDeath()
    {
        bossIsDying = true;
        GetComponentInChildren<Animator>().SetTrigger("Dead");
        yield return new WaitForSeconds(6f);
        Destroy(gameObject);
    }
}
