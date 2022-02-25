using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalBullet : MonoBehaviour
{
    PortalManager portalManager;
    GameObject[] portals = new GameObject[2];

    // Start is called before the first frame update
    void Start()
    {
        portalManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<PortalManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        print(collision.gameObject.name);
        portalManager.AddPos(transform.position);
        Destroy(gameObject);
    }
}
