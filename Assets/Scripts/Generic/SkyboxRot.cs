using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxRot : MonoBehaviour
{
    [SerializeField] float rotateSpeed = 1.1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.deltaTime * rotateSpeed);
    }
}
