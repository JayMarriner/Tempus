using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalBullet : Bullet
{
    PortalManager portalManager;
    GameObject[] portals = new GameObject[2];

    // Start is called before the first frame update
    public override void Start()
    {
        portalManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<PortalManager>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        print(collision.gameObject.name);
        portalManager.AddPos(ColPoint, ColNormal);
        Destroy(gameObject);
    }
}
