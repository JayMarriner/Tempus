using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockRain : MonoBehaviour
{
    [SerializeField] GameObject rocks;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawnrocks());
    }

    IEnumerator Spawnrocks()
    {
        yield return new WaitForSeconds(1f);
        rocks.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
