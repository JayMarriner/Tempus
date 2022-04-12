using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DitzelGames.FastIK;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] GameObject[] weapons;
    [Header("IK setup")]
    [SerializeField] GameObject leftHand;
    [SerializeField] GameObject rightHand;
    [Header("Weapon IK setup")]
    [SerializeField] GameObject[] leftHandTarget;
    [SerializeField] GameObject[] rightHandTarget;
    [SerializeField] GameObject[] leftElbowTarget;
    [SerializeField] GameObject[] rightElbowTarget;
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

        leftHand.GetComponent<FastIKFabric>().enabled = false;
        rightHand.GetComponent<FastIKFabric>().enabled = false;
        anim.SetLayerWeight(1, 0);
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

        if(Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            NextWeapon();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            PrevWeapon();
        }
    }

    public void EnableWeapon(int weaponInt)
    {
        //Sets weapon 0 (fist, no weapon)
        if(weaponInt == 0)
        {
            if (leftHand.GetComponent<FastIKFabric>().enabled)
            {
                leftHand.GetComponent<FastIKFabric>().enabled = false;
                rightHand.GetComponent<FastIKFabric>().enabled = false;
            }

            anim.SetLayerWeight(1, 0);

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
                anim.SetLayerWeight(1, 1);
                if (!leftHand.GetComponent<FastIKFabric>().enabled)
                {
                    leftHand.GetComponent<FastIKFabric>().enabled = true;
                    rightHand.GetComponent<FastIKFabric>().enabled = true;
                }

                leftHand.GetComponent<FastIKFabric>().Target = leftHandTarget[weaponInt - 1].transform;
                rightHand.GetComponent<FastIKFabric>().Target = rightHandTarget[weaponInt - 1].transform;
                leftHand.GetComponent<FastIKFabric>().Pole = leftElbowTarget[weaponInt - 1].transform;
                rightHand.GetComponent<FastIKFabric>().Pole = rightElbowTarget[weaponInt - 1].transform;
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

    void NextWeapon()
    {
        int nextWeapon = currentWeapon+=1;
        if (nextWeapon > weapons.Length)
            nextWeapon = 0;
        EnableWeapon(nextWeapon);
    }

    void PrevWeapon()
    {
        int nextWeapon = currentWeapon-=1;
        if (nextWeapon < 0)
            nextWeapon = weapons.Length;
        EnableWeapon(nextWeapon);
    }
}
