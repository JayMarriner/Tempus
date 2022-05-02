using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jetpack : MonoBehaviour
{
    [Header("Player")]
    private ThirdPersonPlayer tpp;
    private InputManager inputManager;

    [Header("Fuel")]
    [SerializeField] public float maxFuel = 4f;
    [SerializeField] public float currFuel;

    [Header("Effects")]
    public ParticleSystem effect1;
    public ParticleSystem effect2;

    [Header("Bools")]
    public bool isEquipped = false;
    public bool HasJetPack { get; set; }

    [Header("UI")]
    [SerializeField] Image fuelFill;

    // Start is called before the first frame update
    void Start()
    {
        currFuel = maxFuel;

        tpp = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonPlayer>();
        inputManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<InputManager>();

        effect1.Stop();
        effect2.Stop();

        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        TakeFlight();
    }

    void TakeFlight()
    {
        if (isEquipped)
        {
            if (Input.GetKey(inputManager.useJetpack) && currFuel > 0f)
            {
                tpp.usingJetpack = true;
                currFuel -= Time.deltaTime;
                fuelFill.fillAmount = currFuel / maxFuel;
                effect1.Play();
                effect2.Play();
            }

            if (Input.GetKey(inputManager.useJetpack) || currFuel <= 0f)
            {
                tpp.usingJetpack = false;
                effect1.Stop();
                effect2.Stop();
            }

            if (tpp.controller.isGrounded)
                StartCoroutine(Refuel());
        }

    }

    IEnumerator Refuel()
    {
        while (currFuel < maxFuel)
        {
            currFuel += 1f * Time.deltaTime;
            fuelFill.fillAmount = currFuel / maxFuel;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
