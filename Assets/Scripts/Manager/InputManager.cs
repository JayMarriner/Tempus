using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public KeyCode aim;
    public KeyCode shoot;
    public KeyCode sprint;
    public KeyCode weaponWheelToggle;
    public KeyCode weaponSwitch1;
    public KeyCode weaponSwitch2;
    public KeyCode weaponSwitch3;
    public KeyCode useJetpack;
    public KeyCode toggleJetpack;

    private void Awake()
    {
        aim = KeyCode.Mouse1;
        shoot = KeyCode.Mouse0;
        sprint = KeyCode.LeftShift;
        weaponWheelToggle = KeyCode.Tab;
        weaponSwitch1 = KeyCode.Alpha1;
        weaponSwitch2 = KeyCode.Alpha2;
        weaponSwitch3 = KeyCode.Alpha3;
        useJetpack = KeyCode.Space;
        toggleJetpack = KeyCode.X;
        
    }
}
