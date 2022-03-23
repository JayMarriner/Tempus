using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float projectileSpeed = 10f;

    [SerializeField] private Rigidbody body;

    // Start is called before the first frame update
    void Awake()
    {
        body = gameObject.GetComponent<Rigidbody>();
        body.AddForce(Vector3.up * projectileSpeed, ForceMode.Impulse);        
    }

    // Update is called once per frame
    void Update()
    {
        print(body.velocity.y);
    }
}
