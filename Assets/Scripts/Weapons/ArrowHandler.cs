using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHandler : MonoBehaviour
{
    public bool isFired;
    bool lifeStarted;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isFired && !lifeStarted)
            StartCoroutine(LifeTime());
    }

    IEnumerator LifeTime()
    {
        lifeStarted = true;
        yield return new WaitForSeconds(30f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            other.GetComponent<RobotInfo>().LowerHealth(25);
            Destroy(gameObject);
        }

        if(other.tag == "Spawner")
        {
            other.GetComponent<RobotInfo>().LowerHealth(50);
            Destroy(gameObject);
        }
    }
}
