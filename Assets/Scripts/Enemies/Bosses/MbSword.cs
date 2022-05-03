using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MbSword : MonoBehaviour
{
    [SerializeField] MedBoss mainScript;
    bool cooldown;
    // Start is called before the first frame update
    void Start()
    {
        cooldown = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (cooldown == false && mainScript.isHitting && other.tag == "Player")
        {
            cooldown = true;
            other.GetComponent<ThirdPersonPlayer>().TakeDamage(2.5f);
            StartCoroutine(Cool());
        }
    }

    IEnumerator Cool()
    {
        yield return new WaitForSeconds(2f);
        cooldown = false;
    }
}
