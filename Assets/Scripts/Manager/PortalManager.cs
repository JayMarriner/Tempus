using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    [SerializeField] GameObject portalPrefab;
    Vector3 pos1;
    Vector3 pos2;
    bool pos1Set;
    bool pos2Set;
    GameObject portal1;
    GameObject portal2;
    public void AddPos(Vector3 position)
    {
        if (!pos1Set)
        {
            pos1 = position;
            portal1 = Instantiate(portalPrefab);
            portal1.transform.position = pos1;
            pos1Set = true;
        }

        else if (pos1Set && !pos2Set)
        {
            pos2 = position;
            portal2 = Instantiate(portalPrefab);
            portal2.transform.position = pos2;
            pos2Set = true;
            portal1.GetComponent<Teleporter>().targetPos = position;
            portal1.GetComponent<Teleporter>().targetSet = true;
        }

        else
        {
            pos1 = position;
            pos2Set = false;
            Destroy(portal1);
            Destroy(portal2);
            portal1 = Instantiate(portalPrefab);
            portal1.transform.position = pos1;
        }
    }
}
