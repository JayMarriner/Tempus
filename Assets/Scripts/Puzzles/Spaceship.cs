using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spaceship : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    bool inArea;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inArea && Input.GetKey(KeyCode.E))
            SceneManager.LoadScene("MallusFight");
        canvas.SetActive(inArea);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") { }
            inArea = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            inArea = false;
    }
}
