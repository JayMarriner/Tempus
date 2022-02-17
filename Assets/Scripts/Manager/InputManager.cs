using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public KeyCode aim;
    public KeyCode shoot;
    public KeyCode weaponWheelToggle;
    private void Awake()
    {
        aim = KeyCode.Mouse1;
        shoot = KeyCode.Mouse0;
        weaponWheelToggle = KeyCode.Tab;
    }
}
