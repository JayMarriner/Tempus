using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class StartCamera : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera cinCamera;
    [SerializeField] float length;
    [SerializeField] float camSpeed;
    [SerializeField] GameObject canvas;
    [SerializeField] Image fadeIn;
    ThirdPersonPlayer playerScript;

    private void Awake()
    {
        cinCamera.Priority = 20;
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonPlayer>();
        StartCoroutine(CamMove());

    }

    IEnumerator CamMove()
    {
        playerScript.stopMovement = true;
        yield return new WaitForSeconds(1f);
        float opacity = 1f;
        while (opacity > 0)
        {
            fadeIn.fillAmount -= 0.01f;
            opacity -= 0.01f;
            if(fadeIn.fillAmount < 0.9)
                cinCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition += Time.deltaTime * camSpeed / 2;
            yield return new WaitForSeconds(0.002f);
        }
        float currentLength = 0;
        while (currentLength < length)
        {
            cinCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition += Time.deltaTime * camSpeed / 2;
            yield return new WaitForSeconds(0.01f);
            currentLength += 0.01f;
        }
        cinCamera.Priority = 1;
        playerScript.stopMovement = false;
        yield return new WaitForSeconds(0.5f);
        canvas.SetActive(false);
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
