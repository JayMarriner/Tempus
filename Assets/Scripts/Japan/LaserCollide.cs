using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCollide : MonoBehaviour
{
    bool cool;
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
        if(other.tag == "Player" && !cool)
        {
            other.GetComponent<ThirdPersonPlayer>().TakeDamage(2.5f);
            StartCoroutine(Cooldown());
        }
    }

    IEnumerator Cooldown()
    {
        cool = true;
        yield return new WaitForSeconds(2f);
        cool = false;
    }
}
