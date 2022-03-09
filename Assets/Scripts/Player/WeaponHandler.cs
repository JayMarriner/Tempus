using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] GameObject[] weapons;
    InputManager inputManager;
    GameObject currentActive;
    int currentWeapon;
    public int getCurrentWeapon { get => currentWeapon; }
    

    // Start is called before the first frame update
    void Start()
    {
        inputManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<InputManager>();
        currentWeapon = 0;
        foreach (GameObject obj in weapons)
            obj.SetActive(false);
    }

    private void Update()
    {
        //Quick switches.
        if (Input.GetKeyDown(inputManager.weaponSwitch1))
            EnableWeapon(0);
        else if (Input.GetKeyDown(inputManager.weaponSwitch2))
            EnableWeapon(1);
        else if (Input.GetKeyDown(inputManager.weaponSwitch3))
            EnableWeapon(2);
    }

    public void EnableWeapon(int weaponInt)
    {
        if(weaponInt == 0)
        {
            currentWeapon = 0;
            if (currentActive != null)
                currentActive.SetActive(false);
            currentActive = null;
        }
        else
        {
            if (weaponInt - 1 > weapons.Length)
                print("Exceeds amount of weapons...");
            else
            {
                currentWeapon = weaponInt;
                if (currentActive != null)
                    currentActive.SetActive(false);
                weapons[weaponInt - 1].SetActive(true);
                currentActive = weapons[weaponInt - 1];
            }
        }
    }
}
