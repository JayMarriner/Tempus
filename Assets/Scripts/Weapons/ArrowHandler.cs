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
        yield return new WaitForSeconds(60f);
        Destroy(gameObject);
    }
}
