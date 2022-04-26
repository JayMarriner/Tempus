using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLoader : MonoBehaviour
{
    [SerializeField] GameObject[] objects;
    [Range(50,500)]
    [SerializeField] int renderDistance;
    Vector3 playerOldPos;
    Vector3 playerPos;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        for (int x = 0; x < objects.Length; x++)
        {
            objects[x].SetActive(Vector3.Distance(objects[x].transform.position, playerPos) < renderDistance);
        }
        playerOldPos = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        if (Vector3.Distance(playerPos, playerOldPos) > 10)
        {
            for (int x = 0; x < objects.Length; x++)
            {
                objects[x].SetActive(Vector3.Distance(objects[x].transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) < renderDistance);
            }
            playerOldPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        }
    }
}
