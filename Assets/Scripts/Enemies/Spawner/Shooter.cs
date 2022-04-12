using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] int amountAllowedAtOnce;
    [SerializeField] int coolTime;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject spawnPoint;
    public int currentAmt;
    bool coolDown;
    EnemyHandler enemyHandler;
    Vector3 OldPos;

    // Start is called before the first frame update
    void Start()
    {
        enemyHandler = GetComponent<EnemyHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHandler.playerInArea && currentAmt < amountAllowedAtOnce && !coolDown)
        {
            GameObject newBullet = Instantiate(bullet);
            newBullet.transform.position = spawnPoint.transform.position;
            newBullet.transform.rotation = spawnPoint.transform.rotation;
            newBullet.GetComponent<SpawnBullet>().shootScript = this;
            newBullet.GetComponent<Rigidbody>().AddForce(Vector3.up * Time.deltaTime * 10000);
            StartCoroutine(CoolDown());
        }
    }

    IEnumerator CoolDown()
    {
        if (coolDown)
            yield break;
        coolDown = true;
        yield return new WaitForSeconds(coolTime);
        coolDown = false;
    }
}
