using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WheelItems : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PointerEnter(GameObject obj)
    {
        obj.GetComponent<Image>().color = new Color32(100, 100, 100, 100);
    }

    public void PointerExit(GameObject obj)
    {
        obj.GetComponent<Image>().color = new Color32(255, 255, 255, 100);
    }
}
