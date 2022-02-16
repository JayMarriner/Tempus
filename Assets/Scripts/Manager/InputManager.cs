using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public KeyCode aim;
    private void Awake()
    {
        aim = KeyCode.Mouse1;
    }
}
