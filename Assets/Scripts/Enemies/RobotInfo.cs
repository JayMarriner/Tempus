using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class RobotInfo : MonoBehaviour
{
    [SerializeField] float health = 100;
    [SerializeField] Image healthImg;
    [SerializeField] GameObject door;
    [SerializeField] NavMeshAgent agent;
    public float GetHealth { get => health; }
    public Shooter shooterScript;
    ThirdPersonPlayer player;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonPlayer>();
    }

    public void LowerHealth(int amt)
    {
        health -= amt;
        healthImg.fillAmount = health/100;

        if(health <= 0)
        {          
            StartCoroutine(StartLoadToPortalRoom());                         
        }
    }

    IEnumerator StartLoadToPortalRoom()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
        SceneManager.LoadScene(1);        
    }
}
