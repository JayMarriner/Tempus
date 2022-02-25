using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootHandler : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform spawnPoint;
    InputManager inputManager;
    Ray RayOrigin;
    RaycastHit HitInfo;

    private void Start()
    {
        inputManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(-Input.GetAxis("Mouse Y"), 0, 0);
        if (Input.GetKey(inputManager.aim) && Input.GetKeyDown(inputManager.shoot))
        {
            /* GameObject newBullet = Instantiate(bullet);
             newBullet.transform.position = spawnPoint.position;
             newBullet.transform.rotation = spawnPoint.rotation;
             Vector3 hit;

             RayOrigin = Camera.main.ViewportPointToRay(new Vector3(0, 0, 0));
             if (Physics.Raycast(RayOrigin, out HitInfo, 100f))
             {
                 newBullet.transform.LookAt(HitInfo.point);
                 hit = HitInfo.point;
                 print("Transformed...");
                 newBullet.GetComponent<Rigidbody>().AddForce(transform.forward * hit * 100);
             }
             else
                 newBullet.GetComponent<Rigidbody>().AddForce(newBullet.transform.forward * 100);
            */

            /*
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
            RaycastHit hit;
            // Check whether your are pointing to something so as to adjust the direction
            Vector3 targetPoint;
            if (Physics.Raycast(ray, out hit))
                targetPoint = hit.point;
            else
                targetPoint = ray.GetPoint(1000); // You may need to change this value according to your needs
                                                  // Create the bullet and give it a velocity according to the target point computed before
            var shotBullet = Instantiate(bullet, transform.position, transform.rotation);
            shotBullet.GetComponent<Rigidbody>().velocity = (targetPoint - transform.position).normalized * 10;
            */
            GameObject newBullet = Instantiate(bullet, spawnPoint);
            newBullet.transform.parent = null;
            newBullet.GetComponent<Rigidbody>().AddForce(newBullet.transform.forward * 100);
            StartCoroutine(BulletLife(newBullet));
        }
    }

    IEnumerator BulletLife(GameObject obj)
    {
        yield return new WaitForSeconds(4);
        Destroy(obj);
    }
}
