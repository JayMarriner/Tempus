using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalPuzzle : MonoBehaviour
{
    [SerializeField] GameObject walls;
    [SerializeField] GameObject UI;
    [SerializeField] GameObject button;

    bool inArea;
    bool complete;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(inArea && Input.GetKey(KeyCode.E))
        {
            Destroy(walls);
            Destroy(UI);
            button.transform.localPosition += new Vector3(0.05f, 0, 0);
            complete = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !complete)
        {
            inArea = true;
            UI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player" && !complete)
        {
            inArea = false;
            UI.SetActive(false);
        }
    }
}
