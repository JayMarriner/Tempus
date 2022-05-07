using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectCam : MonoBehaviour
{
    Image fadeIn;
    float opacity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FadeIn()
    {
        while (opacity < 1)
        {
            fadeIn.fillAmount += 0.01f;
            opacity += 0.01f;
            yield return new WaitForSeconds(0.002f);
        }
    }
}
