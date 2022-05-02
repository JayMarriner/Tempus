using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Cinemachine;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] Slider audioSlider;
    [SerializeField] Slider camHorSlider;
    [SerializeField] Slider camVerSlider;
    [SerializeField] CinemachineFreeLook cam;
    float currAudioVal;

    [Header("Start values")]
    float startAudio;
    float startCamHor;
    float startCamVer;

    // Start is called before the first frame update
    void Start()
    {
        audioSlider.value = 1;
        startAudio = currAudioVal = 1;
        startCamHor = camHorSlider.value = cam.m_XAxis.m_MaxSpeed;
        startCamVer = camVerSlider.value = cam.m_YAxis.m_MaxSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        AudioListener.volume = audioSlider.value;
        currAudioVal = audioSlider.value;
        cam.m_XAxis.m_MaxSpeed = camHorSlider.value;
        cam.m_YAxis.m_MaxSpeed = camVerSlider.value;
    }

    public void RevertAll()
    {
        currAudioVal = audioSlider.value = startAudio;
        cam.m_XAxis.m_MaxSpeed = camHorSlider.value = startCamHor;
        cam.m_YAxis.m_MaxSpeed = camVerSlider.value = startCamVer;
    }
}
