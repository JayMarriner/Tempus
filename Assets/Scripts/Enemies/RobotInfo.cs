using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using Cinemachine;

public class RobotInfo : MonoBehaviour
{
    [SerializeField] float health = 100;
    [SerializeField] private Image healthImg;
    [SerializeField] private GameObject portal;
    [SerializeField] private CinemachineVirtualCamera cam;
    [SerializeField] private GameObject portal_NPC;
    
    public float GetHealth { get => health; }
    public Shooter shooterScript;
    ThirdPersonPlayer player;


    private void Start()
    {
        portal.gameObject.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonPlayer>();
        portal_NPC.gameObject.SetActive(false);
    }

    public void LowerHealth(int amt)
    {
        health -= amt;
        healthImg.fillAmount = health/100;

        if(health <= 0)
        {          
            StartCoroutine(StartLoadToPortalRoom());
            cam.Priority = 20;
            portal.gameObject.SetActive(true);
            portal_NPC.gameObject.SetActive(true);
        }
    }

    IEnumerator StartLoadToPortalRoom()
    {
        yield return new WaitForSeconds(3);        
        cam.Priority = 0;
        Destroy(gameObject);
    }
}
