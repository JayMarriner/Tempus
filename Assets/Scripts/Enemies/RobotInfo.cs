using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotInfo : MonoBehaviour
{
    float health = 100;
    public Shooter shooterScript;

    private void Update()
    {
        
    }

    public void LowerHealth(int amt)
    {
        health -= amt;
        if(health <= 0)
        {
            shooterScript.currentAmt--;
            Destroy(gameObject);
        }
    }
}
