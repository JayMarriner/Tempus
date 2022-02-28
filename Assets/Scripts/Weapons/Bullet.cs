using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 ColPoint { get; set; }
    public Vector3 ColNormal { get; set; }
    
    public virtual void Start()
    {
        StartCoroutine(LifeSpan());
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    IEnumerator LifeSpan()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
