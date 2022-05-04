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

    public bool cntrl;

    public void AddPos(Vector3 position, Vector3 normal)
    {
        /*
        if (!pos1Set)
        {
            print("ping1");
            pos1 = position;
            portal1 = Instantiate(portalPrefab);
            portal1.transform.position = pos1;
            //portal1.transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
            //portal1.transform.LookAt(normal);
            portal1.transform.rotation = Quaternion.LookRotation(normal, Vector3.up);
            portal1.GetComponent<Teleporter>().cams[1].SetActive(false);
            pos1Set = true;
        }

        else if (pos1Set && !pos2Set)
        {
            print("ping2");
            pos2 = position;
            portal2 = Instantiate(portalPrefab);
            portal2.transform.position = pos2;
            //portal2.transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
            portal2.transform.rotation = Quaternion.LookRotation(normal, Vector3.up);
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

        else if (pos1Set && pos2Set)
        {
            if (portal2 != null)
            {
                Destroy(portal2);
                pos2 = position;
                portal2 = Instantiate(portalPrefab);
                portal2.transform.position = pos2;
                //portal2.transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
                portal2.transform.rotation = Quaternion.LookRotation(normal, Vector3.up);
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
                Destroy(portal1);
                Destroy(portal2);
                pos1 = position;
                portal1 = Instantiate(portalPrefab);
                portal1.transform.position = pos1;
                portal1.transform.rotation = Quaternion.LookRotation(normal, Vector3.up);
            }
        }*/

        if (!pos1Set && !cntrl)
        {
            pos1 = position;
            portal1 = Instantiate(portalPrefab);
            portal1.transform.position = pos1;
            //portal1.transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
            //portal1.transform.LookAt(normal);
            portal1.transform.rotation = Quaternion.LookRotation(normal, Vector3.up);
            portal1.GetComponent<Teleporter>().cams[1].SetActive(false);
            portal1.GetComponent<Teleporter>().isFirst = true;
            pos1Set = true;
        }
        else if(pos1Set && !cntrl)
        {
            Destroy(portal1);
            pos1 = position;
            portal1 = Instantiate(portalPrefab);
            portal1.transform.position = pos1;
            //portal1.transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
            //portal1.transform.LookAt(normal);
            portal1.transform.rotation = Quaternion.LookRotation(normal, Vector3.up);
            portal1.GetComponent<Teleporter>().cams[1].SetActive(false);
            portal1.GetComponent<Teleporter>().isFirst = true;
            pos1Set = true;
        }

        if(!pos2Set && cntrl)
        {
            pos2 = position;
            portal2 = Instantiate(portalPrefab);
            portal2.transform.position = pos2;
            //portal2.transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
            portal2.transform.rotation = Quaternion.LookRotation(normal, Vector3.up);
            pos2Set = true;
        }
        else if (pos2Set && cntrl)
        {
            Destroy(portal2);
            pos2 = position;
            portal2 = Instantiate(portalPrefab);
            portal2.transform.position = pos2;
            //portal2.transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
            portal2.transform.rotation = Quaternion.LookRotation(normal, Vector3.up);
            pos2Set = true;
        }

        if(pos1Set && pos2Set)
        {
            portal1.GetComponent<Teleporter>().targetPos = position;
            if (portal1.GetComponent<Teleporter>().targetPos.y < 1)
                portal1.GetComponent<Teleporter>().targetPos.y = 1;
            portal1.GetComponent<Teleporter>().targetSet = true;
            portal2.GetComponent<Teleporter>().cams[0].SetActive(false);
            //portal1.GetComponentInChildren<Material>().SetTexture("tex1", renderTextures[0]);
            portal1.GetComponentInChildren<MeshRenderer>().material = new Material(portalMats[1]);
            portal2.GetComponentInChildren<MeshRenderer>().material = new Material(portalMats[0]);
        }
    }
}
