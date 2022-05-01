using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotInfo : MonoBehaviour
{
    [SerializeField] float health = 100;
    [SerializeField] Image healthImg;
    public float GetHealth { get => health; }
    public Shooter shooterScript;

    private void Update()
    {
        
    }

    public void LowerHealth(int amt)
    {
        health -= amt;
        healthImg.fillAmount = health/100;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
