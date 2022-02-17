using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootHandler : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform spawnPoint;
    InputManager inputManager;

    private void Start()
    {
        inputManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(-Input.GetAxis("Mouse Y"), 0, 0);
        if(Input.GetKey(inputManager.aim) && Input.GetKeyDown(inputManager.shoot))
        {
            GameObject newBullet = Instantiate(bullet, spawnPoint);
            newBullet.GetComponent<Rigidbody>().AddForce(transform.forward * 100);
            newBullet.transform.parent = null;
            StartCoroutine(BulletLife(newBullet));
        }
    }

    IEnumerator BulletLife(GameObject obj)
    {
        yield return new WaitForSeconds(4);
        Destroy(obj);
    }
}
