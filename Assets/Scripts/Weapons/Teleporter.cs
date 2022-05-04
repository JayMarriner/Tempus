using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Vector3 targetPos;
    public bool targetSet;
    [SerializeField] public GameObject[] cams = new GameObject[2];
    [SerializeField] GameObject p1;
    [SerializeField] GameObject p2;
    public bool isFirst;

    private void Start()
    {
        if (isFirst)
            p1.SetActive(true);
        else
            p2.SetActive(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        //If the collider is the player then...
        if (other.gameObject.CompareTag("Player") && targetSet)
        {
            //Turn off character controller, necessary for teleporting.
            other.gameObject.GetComponent<CharacterController>().enabled = false;
            //Move player position to match the targetted position (portal 2).
            other.gameObject.transform.position = targetPos;
            //Turn character controller back on.
            other.gameObject.GetComponent<CharacterController>().enabled = true;
        }
    }

    private void Update()
    {
        //Rotate cameras on the portals to match the players rotation.
        if (cams[0].activeSelf)
            cams[0].transform.localRotation = Camera.main.transform.rotation;
        else if(cams[1].activeSelf)
            cams[1].transform.localRotation = Camera.main.transform.rotation;
    }
}
