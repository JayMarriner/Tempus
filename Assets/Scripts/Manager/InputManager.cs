using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public KeyCode aim;
    public KeyCode shoot;
    public KeyCode weaponWheelToggle;
    public KeyCode weaponSwitch1;
    public KeyCode weaponSwitch2;
    public KeyCode sprint;
    private void Awake()
    {
        aim = KeyCode.Mouse1;
        shoot = KeyCode.Mouse0;
        weaponWheelToggle = KeyCode.Tab;
        weaponSwitch1 = KeyCode.Alpha1;
        weaponSwitch2 = KeyCode.Alpha2;
        sprint = KeyCode.LeftShift;
    }
}
