using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    [SerializeField] GameObject portalPrefab;
    [SerializeField] Material[] portalMats = new Material[2];
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
            portal1.transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
            portal1.GetComponent<Teleporter>().cams[1].SetActive(false);
            pos1Set = true;
        }

        else if (pos1Set && !pos2Set)
        {
            pos2 = position;
            portal2 = Instantiate(portalPrefab);
            portal2.transform.position = pos2;
            portal2.transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
            pos2Set = true;
            portal1.GetComponent<Teleporter>().targetPos = position;
            if (portal1.GetComponent<Teleporter>().targetPos.y < 1)
                portal1.GetComponent<Teleporter>().targetPos.y = 1;
            portal1.GetComponent<Teleporter>().targetSet = true;
            portal2.GetComponent<Teleporter>().cams[0].SetActive(false);
            //portal1.GetComponentInChildren<Material>().SetTexture("tex1", renderTextures[0]);
            portal1.GetComponentInChildren<MeshRenderer>().material = new Material(portalMats[1]);
            portal2.GetComponentInChildren<MeshRenderer>().material = new Material(portalMats[0]);
        }

        else
        {
            pos1 = position;
            pos2Set = false;
            Destroy(portal1);
            Destroy(portal2);
            portal1 = Instantiate(portalPrefab);
            portal1.transform.position = pos1;
            portal1.transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
        }
    }
}
