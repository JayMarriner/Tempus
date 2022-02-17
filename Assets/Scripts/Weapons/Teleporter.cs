using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Vector3 targetPos;
    public bool targetSet;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && targetSet)
        {
            other.gameObject.GetComponent<CharacterController>().enabled = false;
            other.gameObject.transform.position = targetPos;
            other.gameObject.GetComponent<CharacterController>().enabled = true;
        }
    }
}
