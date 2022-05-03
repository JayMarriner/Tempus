using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MbSword : MonoBehaviour
{
    bool cooldown;
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
        if (cooldown = false && other.tag == "Player")
        {
            cooldown = true;
            other.GetComponent<ThirdPersonPlayer>().TakeDamage(2.5f);
        }
    }

    IEnumerator Cool()
    {
        yield return new WaitForSeconds(2f);
        cooldown = false;
    }
}
