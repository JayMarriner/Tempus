using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetWeapons : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager gm = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        for(int x = 0; x < gm.weaponsActive.Length; x++)
        {
            if(x > 0)
                gm.weaponsActive[x] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
