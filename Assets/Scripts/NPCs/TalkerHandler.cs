using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class TalkerHandler : MonoBehaviour
{
    [SerializeField] GameObject playerStand;
    [SerializeField] GameObject playerFace;
    [SerializeField] GameObject cutsceneImgs;
    [SerializeField] GameObject alertCanvas;
    [SerializeField] CinemachineVirtualCamera cinCamera;
    [SerializeField] Image blackFade;

    [Header("Camera control settings")]
    [Range(1,10)]
    [SerializeField] int camSpeed;

    bool playerInArea;
    bool inCutscene;
    InputManager inputManager;
    ThirdPersonPlayer playerScript;
    GameObject player;
    [SerializeField] NPCText npcText;

    // Start is called before the first frame update
    void Start()
    {
        inputManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<InputManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<ThirdPersonPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInArea && Input.GetKeyDown(inputManager.interact) && !inCutscene)
        {
            inCutscene = true;
            StartCoroutine(Fader(true));
            StartCoroutine(ControlCam());
        }
    }

    public void EndConversation()
    {
        StartCoroutine(Fader(false));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            playerInArea = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            playerInArea = false;
    }

    IEnumerator Fader(bool startConv)
    {
        if (startConv)
        {
            alertCanvas.SetActive(false);
        }
        while(blackFade.fillAmount < 1f)
        {
            blackFade.fillAmount += 0.02f;
            yield return new WaitForSeconds(0.01f);
        }
        cutsceneImgs.SetActive(startConv);
        playerScript.stopMovement = startConv;
        playerScript.stopCamMove = startConv;
        player.GetComponent<CharacterController>().enabled = !startConv;
        player.transform.position = new Vector3(playerStand.transform.position.x, player.transform.position.y, playerStand.transform.position.z);
        player.transform.LookAt(playerFace.transform);
        if (startConv)
            cinCamera.Priority = 20;
        else
            cinCamera.Priority = 1;

        yield return new WaitForSeconds(1f);

        while(blackFade.fillAmount > 0f)
        {
            blackFade.fillAmount -= 0.02f;
            yield return new WaitForSeconds(0.01f);
        }

        if(startConv)
            npcText.StartRead();
        else
            cinCamera.enabled = false;
    }

    IEnumerator ControlCam()
    {
        while(inCutscene)
        {
            cinCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition += Time.deltaTime * camSpeed / 2;
            yield return new WaitForSeconds(0.001f);
        }
    }
}
