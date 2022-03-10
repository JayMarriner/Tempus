using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{   
    public GameObject enemyProjectile;
    public Transform spawnPoint;
    public int activeEnemies;
    public int maxSpawnEnemies = 5;    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && activeEnemies < maxSpawnEnemies) 
        {
            Debug.Log("Player Has Entered Range!");
            GameObject newEnemy = (GameObject)Instantiate(enemyProjectile, spawnPoint.transform.position, Quaternion.identity);
            SpawnEnemy spawn = newEnemy.GetComponentInChildren<SpawnEnemy>();
            spawn.Spawner = this;
            activeEnemies++;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Has Exited Range!");
        }
    }
   
}
