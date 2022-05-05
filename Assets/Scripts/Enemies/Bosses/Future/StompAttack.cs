using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompAttack : MonoBehaviour
{
    ParticleSystem attack;
    Vector3 initPos;
    bool setOff;

    // Start is called before the first frame update
    void Start()
    {
        initPos = gameObject.transform.localPosition;
        GetComponent<BoxCollider>().enabled = false;
        attack = GetComponentInParent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (attack.isPlaying && !setOff)
        {
            GetComponent<BoxCollider>().enabled = true;
            setOff = true;
            gameObject.transform.localPosition = initPos;
            StartCoroutine(Move());
        }

    }

    IEnumerator Move()
    {
        float counter = 0;
        while (counter < 3)
        {
            transform.localPosition += new Vector3(0, 0, 1f);
            yield return new WaitForSeconds(0.01f);
            counter += 0.01f;
        }
        GetComponent<BoxCollider>().enabled = false;
        while (attack.isPlaying)
            yield return new WaitForSeconds(0.2f);
            setOff = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            other.GetComponent<ThirdPersonPlayer>().TakeDamage(2.5f);
    }
}
