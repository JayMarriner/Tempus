using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBotHandler : EnemyHandler
{
    RobotInfo robotInfo;

    // Start is called before the first frame update
    void Start()
    {
        robotInfo = GetComponent<RobotInfo>();
    }

}
