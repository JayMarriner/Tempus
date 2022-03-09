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
        if (playerScript.shoulderView && weaponHandler.getCurrentWeapon == 1 && !reticle.activeSelf)
            StartCoroutine(ActivateReticle());
        if (!playerScript.shoulderView && reticle.activeSelf)
            reticle.SetActive(false);
        if (playerScript.shoulderView && weaponHandler.getCurrentWeapon != 1 && reticle.activeSelf)
            reticle.SetActive(false);
    }

    void WeaponUI()
    {
        if (Input.GetKeyDown(inputManager.weaponWheelToggle))
        {
            WeaponWheel.SetActive(true);
            playerScript.stopCamMove = true;
            Time.timeScale = 0.1f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if (Input.GetKeyUp(inputManager.weaponWheelToggle))
        {
            WeaponWheel.SetActive(false);
            playerScript.stopCamMove = false;
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }


    IEnumerator ActivateReticle()
    {
        yield return new WaitForSeconds(1.2f);
        reticle.SetActive(playerScript.shoulderView);
    }
}
