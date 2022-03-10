using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootHandler : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform spawnPoint;
    [Range(10, 200)]
    [SerializeField] int bulletSpeed;
    InputManager inputManager;
    Ray RayOrigin;
    RaycastHit HitInfo;
    Vector3 collisionNormal;
    private void Start()
    {
        inputManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(inputManager.aim) && Input.GetKeyDown(inputManager.shoot))
        {
            //Middle of screen.
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
            RaycastHit hit;
            // Check whether your are pointing to something so as to adjust the direction
            Vector3 targetPoint;
            if (Physics.Raycast(ray, out hit))
            {
                //set hit point and collision information.
                targetPoint = hit.point;
                collisionNormal = hit.normal;
                //Debug.DrawLine(collisionNormal, collisionNormal.normalized * 5, Color.red, 5);
            }
            else
                targetPoint = ray.GetPoint(1000); // Gets point up to certain distance.
            
            // Create the bullet, pass through information and give it a velocity according to the target point computed before
            var shotBullet = Instantiate(bullet, spawnPoint.transform.position, transform.rotation);
            shotBullet.GetComponent<Bullet>().ColPoint = targetPoint;
            shotBullet.GetComponent<Bullet>().ColNormal = collisionNormal;
            shotBullet.GetComponent<Rigidbody>().velocity = (targetPoint - shotBullet.transform.position).normalized * bulletSpeed;
        }
    }
}
