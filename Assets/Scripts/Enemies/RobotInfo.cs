using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotInfo : MonoBehaviour
{
    [SerializeField] float health = 100;
    public float GetHealth { get => health; }
    public Shooter shooterScript;

    private void Update()
    {
        
    }

    public void LowerHealth(int amt)
    {
        health -= amt;
        if(health <= 0)
        {
            //shooterScript.currentAmt--;
            Destroy(gameObject);
        }
    }
}
