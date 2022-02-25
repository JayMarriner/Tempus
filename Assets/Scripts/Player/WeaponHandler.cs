using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] GameObject[] weapons;
    InputManager inputManager;
    GameObject currentActive;
    int currentWeapon;
    

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
    }

    public void EnableWeapon(int weaponInt)
    {
        if (weaponInt > weapons.Length-1)
            print("Exceeds weapon amount...");
        else if(weaponInt == 0)
        {
            currentWeapon = 0;
            if (currentActive != null)
                currentActive.SetActive(false);
            currentActive = null;
        }
        else
        {
            currentWeapon = weaponInt;
            weapons[weaponInt-1].SetActive(true);
            if(currentActive != null)
                currentActive.SetActive(false);
            currentActive = weapons[weaponInt-1];
        }
    }
}
