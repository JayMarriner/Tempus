using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotInfo : MonoBehaviour
{
    [SerializeField] float health = 100;
    [SerializeField] Image healthImg;
    [SerializeField] bool isDummy;
    [SerializeField] GameObject door;
    public float GetHealth { get => health; }
    public Shooter shooterScript;
    ThirdPersonPlayer player;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonPlayer>();
    }
    private void Update()
    {
        
    }

    public void LowerHealth(int amt)
    {
        health -= amt;
        healthImg.fillAmount = health/100;
        if(health <= 0)
        {
            if (isDummy)
            {
                Destroy(door);
                //Camera stuff can go here.
                //player.stopMovement = true;
            }
            Destroy(gameObject);
        }
    }
}
