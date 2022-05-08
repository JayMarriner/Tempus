using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayStart : MonoBehaviour
{
    [SerializeField] MallusMech mm;
    [SerializeField] GameObject startCam;
    // Start is called before the first frame update
    void Awake()
    {
        mm.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (startCam == null)
            mm.enabled = true;
    }
}
