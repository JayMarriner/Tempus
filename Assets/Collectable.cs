using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collectable : MonoBehaviour
{
    public enum ObjType
    {
        jetpack,
        bow,
        katana,
        portal,
        scifi,
        jumpBoots
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
    [SerializeField] GameObject jumpBoots;

    [Header("Canvas'")]
    [SerializeField] GameObject bowCanvas;
    [SerializeField] GameObject katanaCanvas;
    [SerializeField] GameObject portalCanvas;
    [SerializeField] GameObject scifiCanvas;
    [SerializeField] GameObject jetpackCanvas;
    [SerializeField] GameObject jumpBootsCanvas;
    [SerializeField] GameObject fader;
    [SerializeField] Image fadeIn;

    GameManager manager;
    GameObject currentActive;
    bool inVideo;
    
    

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
            case ObjType.jumpBoots:
                jumpBoots.SetActive(true);
                break;
        }
    }

    private void Update()
    {
        //Video handler
        if(inVideo && Input.GetKey(KeyCode.Return))
        {
            inVideo = false;
            currentActive.SetActive(false);
            Time.timeScale = 1;
            Destroy(gameObject);
        }

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
                Video(jetpackCanvas, other.gameObject);
            }
            else if(!inVideo)
            {
                switch (type)
                {
                    case ObjType.bow:
                        manager.weaponsActive[1] = true;
                        Video(bowCanvas, other.gameObject);
                        break;
                    case ObjType.katana:
                        manager.weaponsActive[4] = true;
                        Video(katanaCanvas, other.gameObject);
                        break;
                    case ObjType.portal:
                        manager.weaponsActive[2] = true;
                        Video(portalCanvas, other.gameObject);
                        break;
                    case ObjType.scifi:
                        manager.weaponsActive[3] = true;
                        Video(scifiCanvas, other.gameObject);
                        break;
                    case ObjType.jumpBoots:
                        other.GetComponent<JumpBoots>().ShoesOn();
                        Video(jumpBootsCanvas, other.gameObject);
                        break;
                }
            }
        }
    }

    void Video(GameObject vid, GameObject player)
    {
        player.GetComponent<ThirdPersonPlayer>().stopMovement = true;
        StartCoroutine(FadeIn(vid, player));
    }

    IEnumerator FadeIn(GameObject vid, GameObject player)
    {
        fader.SetActive(true);
        float opacity = 0f;
        while (opacity < 1)
        {
            fadeIn.fillAmount += 0.01f;
            opacity += 0.01f;
            yield return new WaitForSeconds(0.002f);
        }
        currentActive = vid;
        vid.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        while (opacity > 0)
        {
            fadeIn.fillAmount -= 0.01f;
            opacity -= 0.01f;
            yield return new WaitForSeconds(0.002f);
        }
        fader.SetActive(false);
        player.GetComponent<ThirdPersonPlayer>().stopMovement = false;
        Time.timeScale = 0;
        inVideo = true;
    }
}
