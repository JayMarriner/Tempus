using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnEnemy : MonoBehaviour
{
    public EnemySpawner Spawner { get; set; }

    [SerializeField] private GameObject spawnBall;
    [SerializeField] private GameObject spawnEnemy;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        spawnEnemy.SetActive(false);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!spawnEnemy.activeSelf)
        {
            spawnEnemy.SetActive(true);
            spawnEnemy.AddComponent<NavMeshAgent>();
            spawnBall.SetActive(false);
            rb.isKinematic = true;
            rb.useGravity = false;
        }
    }
}
