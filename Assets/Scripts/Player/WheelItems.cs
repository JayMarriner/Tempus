using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WheelItems : MonoBehaviour
{
    public void PointerEnter(GameObject obj)
    {
        obj.GetComponent<Image>().color = new Color32(100, 100, 100, 100);
    }

    public void PointerExit(GameObject obj)
    {
        obj.GetComponent<Image>().color = new Color32(255, 255, 255, 100);
    }

    private void OnEnable()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<Image>().color = new Color32(255, 255, 255, 100);
        }
    }
}
