using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using Cinemachine;
using UnityEngine.SceneManagement;

public class Opener : MonoBehaviour
{
    [SerializeField] AudioClip[] clips;
    [SerializeField] string[] dialogue;
    [SerializeField] float[] timeToNext;
    [SerializeField] TMP_Text UItext;
    [SerializeField] AudioSource listen;
    [SerializeField] private CinemachineVirtualCamera cam;
    [SerializeField] GameObject secondCamUI;
    [SerializeField] AudioClip[] afterMallus;
    [SerializeField] string[] afterDialogue;
    [SerializeField] float[] afterTime;
    [SerializeField] TMP_Text afterText;
    [SerializeField] AudioClip bgMusic;
    [SerializeField] AudioSource bgSource;
    [SerializeField] CinemachineVirtualCamera finalCam;
    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(PlayScene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator PlayScene()
    {
        int counter = 0;
        while(counter < clips.Length)
        {
            listen.clip = clips[counter];
            listen.Play();
            yield return new WaitForSeconds(clips[counter].length);
            yield return new WaitForSeconds(timeToNext[counter]);
            counter++;
        }
        secondCamUI.SetActive(true);
        cam.Priority = 20;
        int counter2 = 0;
        bgSource.clip = bgMusic;
        bgSource.Play();
        while(counter2 < afterMallus.Length)
        {
            listen.clip = afterMallus[counter2];
            listen.Play();
            yield return new WaitForSeconds(afterMallus[counter2].length);
            yield return new WaitForSeconds(afterTime[counter2]);
            counter2++;
        }
        yield return new WaitForSeconds(1f);
        secondCamUI.SetActive(false);
        finalCam.Priority = 40;
        finalCam.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = 1;
        yield return new WaitForSeconds(2f);
        while(finalCam.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition > 0)
        {
            finalCam.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition -= 0.001f;
            yield return new WaitForSeconds(0.005f);
        }
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("TutorialLevel");
    }
}
