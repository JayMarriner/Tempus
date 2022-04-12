using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnBullet : MonoBehaviour
{
    [SerializeField] GameObject robot;
    public Shooter shootScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject newRobot = Instantiate(robot);
        newRobot.GetComponent<NavMeshAgent>().enabled = false;
        newRobot.transform.position = gameObject.transform.position;
        newRobot.GetComponent<NavMeshAgent>().enabled = true;
        newRobot.GetComponent<RobotInfo>().shooterScript = this.shootScript;
        shootScript.currentAmt++;
        Destroy(gameObject);
    }
}
