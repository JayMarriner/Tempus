using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool[] weaponsActive = { true, false, false, false, false };

    static GameManager _instance;
    public static GameManager instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject manager = GameObject.FindGameObjectWithTag("Manager");
                _instance = manager.AddComponent<GameManager>();
                DontDestroyOnLoad(manager);
            }
            return _instance;
        }
    }

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            if (this != _instance)
                Destroy(this.gameObject);
        }
    }
}
