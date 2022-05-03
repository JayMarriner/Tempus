using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalKey : MonoBehaviour
{
    GameManager manager;
    [SerializeField] GameObject portal;
    bool alreadyTriggered;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponentInChildren<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !alreadyTriggered)
        {
            int x = 0;
            while (manager.portalsActive[x] == true)
            {
                x++;
            }
            alreadyTriggered = true;
            manager.portalsActive[x] = true;
            portal.SetActive(true);
            Destroy(gameObject);
        }
    }
}
