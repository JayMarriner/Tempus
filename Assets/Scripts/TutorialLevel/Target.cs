using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Target : MonoBehaviour
{
    [SerializeField] ThirdPersonPlayer player;

    [SerializeField] private GameObject doorObject;
    [SerializeField] private GameObject targetObject;

    [SerializeField] private CinemachineVirtualCamera cam;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonPlayer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet" || other.tag == "Arrow")
        {
            cam.Priority = 20;
            Destroy(doorObject);
            player.stopMovement = true;
            StartCoroutine(ChangeCameraPriority());
        }
    }

    IEnumerator ChangeCameraPriority()
    { 
        yield return new WaitForSeconds(5f);

        cam.Priority = 0;

        player.stopMovement = false;
    }
}
