using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        StartCoroutine(ForceRestart());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    IEnumerator ForceRestart()
    {
        yield return new WaitForSeconds(5f);
        if(gameObject.activeSelf)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
