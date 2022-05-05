using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public enum ObjType
    {
        jetpack,
        bow,
        katana,
        portal,
        scifi
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

    [Header("Different objects")]
    [SerializeField] GameObject bow;
    [SerializeField] GameObject katana;
    [SerializeField] GameObject portal;
    [SerializeField] GameObject scifi;
    [SerializeField] GameObject jetpack;

    GameManager manager;
    
    

    private void Start()
    {
        startPos = transform.position.y;
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponentInChildren<GameManager>();

        switch (type)
        {
            case ObjType.bow:
                bow.SetActive(true);
                break;
            case ObjType.katana:
                katana.SetActive(true);
                break;
            case ObjType.portal:
                portal.SetActive(true);
                break;
            case ObjType.scifi:
                scifi.SetActive(true);
                break;
            case ObjType.jetpack:
                jetpack.SetActive(true);
                break;
        }
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
            else
            {
                switch (type)
                {
                    case ObjType.bow:
                        manager.weaponsActive[1] = true;
                        break;
                    case ObjType.katana:
                        manager.weaponsActive[4] = true;
                        break;
                    case ObjType.portal:
                        manager.weaponsActive[2] = true;
                        break;
                    case ObjType.scifi:
                        manager.weaponsActive[3] = true;
                        break;
                }
            }
            Destroy(gameObject);
        }
    }
}
