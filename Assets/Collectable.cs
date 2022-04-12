using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public enum ObjType
    {
        jetpack,
        bow
    };

    [Header("Collectable")]
    [SerializeField] private GameObject collectableObject;

    [Header("Object Type")]
    [SerializeField] ObjType type;    

    [Header("Collectable Movement")]
    [SerializeField] private float xAngle, yAngle, zAngle;
    [SerializeField] private bool collectableDown;
    [SerializeField] private float changeAmt; 
    [SerializeField] private float startPos;
    public float speed = 1f;
    
    

    private void Start()
    {
        startPos = transform.position.y;

    }

    private void Update()
    {
        //Spin collectable
        gameObject.transform.Rotate(xAngle, yAngle, zAngle, Space.World);

        //Bob Up & Down       

        float currPos = transform.localPosition.y;

        if (currPos < startPos + changeAmt && collectableDown == false)
        {
            transform.position += new Vector3(0f, Time.deltaTime * speed, 0f);                       
        }

        if (currPos >= startPos + changeAmt)
        {
            collectableDown = true;
        }

        if (collectableDown)
        {
            if (currPos <= startPos)
            {
                collectableDown = false;
                return;
            }

            transform.position -= new Vector3(0f, Time.deltaTime * speed / 2, 0f);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(type == ObjType.jetpack)
            {
                other.GetComponent<ThirdPersonPlayer>().jetScript.HasJetPack = true;
                other.GetComponent<ThirdPersonPlayer>().jetScript.isEquipped = true;
            }
            Destroy(collectableObject);
        }
    }
}
