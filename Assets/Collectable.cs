using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private GameObject collectableObject;

    [SerializeField] public bool isEquipped;

    private void Start()
    {
        isEquipped = false;
    }    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Set jetpack on player to active here.
            isEquipped = true;
            Destroy(collectableObject);
        }
        else
            isEquipped = false;
    }
}
