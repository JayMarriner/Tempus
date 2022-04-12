using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIHandler : MonoBehaviour
{
    [SerializeField] GameObject WeaponWheel;
    [SerializeField] GameObject reticle;
    [SerializeField] WeaponHandler weaponHandler;
    ThirdPersonPlayer playerScript;
    InputManager inputManager;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = gameObject.GetComponent<ThirdPersonPlayer>();
        inputManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        WeaponUI();
        //If the weapon is a gun and player is in shoulder view and the reticle isnt already active then activate the reticle.
        if (playerScript.shoulderView && weaponHandler.getCurrentWeapon != 1 && !reticle.activeSelf)
            StartCoroutine(ActivateReticle());
        //if the player isn't in shoulderview and the reticle is active then turn if off.
        else if (!playerScript.shoulderView && reticle.activeSelf)
            reticle.SetActive(false);
        //If the player is in shoulder view and the weapon isn't a gun but the reticle is turned on then turn it off.
        else if (playerScript.shoulderView && weaponHandler.getCurrentWeapon == 1 && reticle.activeSelf)
            reticle.SetActive(false);
    }

    void WeaponUI()
    {
        //IF the weapon wheel button is pressed then...
        if (Input.GetKeyDown(inputManager.weaponWheelToggle))
        {
            //Set UI object to active.
            WeaponWheel.SetActive(true);
            //Stop camera movement, this is because the mouse is active and would induce sickness.
            playerScript.stopCamMove = true;
            //Slow down time.
            Time.timeScale = 0.1f;
            //Enable mouse.
            Cursor.lockState = CursorLockMode.None;
            //Set the mouse to visible.
            Cursor.visible = true;
        }
        //If the weapon wheel button is released then...
        if (Input.GetKeyUp(inputManager.weaponWheelToggle))
        {
            //Disable weapon wheel object.
            WeaponWheel.SetActive(false);
            //Enable camera movement again.
            playerScript.stopCamMove = false;
            //Set time back to normal.
            Time.timeScale = 1f;
            //Lock the cursor.
            Cursor.lockState = CursorLockMode.Locked;
            //Set the cursor to invisble.
            Cursor.visible = false;
        }
    }


    IEnumerator ActivateReticle()
    {
        //Wait for camera transition.
        yield return new WaitForSeconds(1.2f);
        //Set reticle to active.
        reticle.SetActive(playerScript.shoulderView);
    }
}
