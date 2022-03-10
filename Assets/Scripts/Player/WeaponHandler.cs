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
        //Sets weapon 0 (fist, no weapon)
        if(weaponInt == 0)
        {
            //Set currently set weapon to disabled if there is one enabled.
            if (currentActive != null)
                currentActive.SetActive(false);
            currentActive = null;
        }
        else
        {
            //Fail safe for exceeding the weapons limit, if you get this error most likely the arrays are wrong in the editor, probably won't be code related.
            if (weaponInt - 1 > weapons.Length)
                print("Exceeds amount of weapons...");
            //Else we set the corrosponding weapon that was passed through to active and the last weapon to inactive.
            else
            {
                //If there's currently an active item then we will set it to false.
                if (currentActive != null)
                    currentActive.SetActive(false);
                //Set the weapon passed through to enabled.
                weapons[weaponInt - 1].SetActive(true);
                //Change the stored current weapon.
                currentActive = weapons[weaponInt - 1];
            }
        }
        currentWeapon = weaponInt;
    }
}
