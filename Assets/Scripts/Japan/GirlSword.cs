using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlSword : MonoBehaviour
{
    [SerializeField] JapanGirlHandler jgh;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            if (jgh.swing)
            {
                other.GetComponent<RobotInfo>().LowerHealth(50);
                jgh.swing = false;
            }
        }
    }
}
