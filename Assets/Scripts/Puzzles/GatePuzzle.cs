using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GatePuzzle : MonoBehaviour
{
    [SerializeField] GameObject door;
    [SerializeField] CinemachineVirtualCamera cam;
    [SerializeField] GameObject bob1;
    [SerializeField] GameObject bob2;
    ThirdPersonPlayer player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Arrow")
        {
            bob1.SetActive(false);
            bob2.SetActive(true);
            Destroy(door);
            cam.Priority = 20;
            player.stopMovement = true;
            StartCoroutine(CamLength());
        }
    }

    IEnumerator CamLength()
    {
        yield return new WaitForSeconds(5f);
        cam.Priority = 0;
        player.stopMovement = false;
    }
}
