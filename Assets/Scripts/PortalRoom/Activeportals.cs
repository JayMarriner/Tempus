using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activeportals : MonoBehaviour
{
    [SerializeField] GameObject[] portals;
    GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("Manager").GetComponentInChildren<GameManager>();
        for(int x = 0; x < portals.Length; x++)
        {
            if (gm.portalsActive[x])
                portals[x].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
