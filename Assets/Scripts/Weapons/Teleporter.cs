using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Vector3 targetPos;
    public bool targetSet;
    [SerializeField] public GameObject[] cams = new GameObject[2];
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && targetSet)
        {
            other.gameObject.GetComponent<CharacterController>().enabled = false;
            other.gameObject.transform.position = targetPos;
            other.gameObject.GetComponent<CharacterController>().enabled = true;
        }
    }

    private void Update()
    {
        if (cams[0].activeSelf)
            cams[0].transform.localRotation = Camera.main.transform.rotation;
        else if(cams[1].activeSelf)
            cams[1].transform.localRotation = Camera.main.transform.rotation;
    }
}
