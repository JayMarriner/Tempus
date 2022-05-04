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
        //Extension of the Bullet class.
        base.Start();
        portalManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<PortalManager>();
        if (Input.GetKey(KeyCode.LeftControl))
        {
            portalManager.cntrl = true;
        }
        else
            portalManager.cntrl = false;
    }


    private void OnCollisionEnter(Collision collision)
    {
        //Add portal at point.
        portalManager.AddPos(ColPoint, ColNormal);
        //Destroy this bullet.
        Destroy(gameObject);
    }
}
