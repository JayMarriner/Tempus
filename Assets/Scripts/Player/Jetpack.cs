using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jetpack : MonoBehaviour
{
    [Header("Player")]
    private ThirdPersonPlayer tpp;
    private InputManager inputManager;

    [Header("Fuel")]
    [SerializeField] private float maxFuel = 4f;
    [SerializeField] private float currFuel;

    [Header("Effects")]
    public ParticleSystem effect1;
    public ParticleSystem effect2;

    [Header("Bools")]
    public bool isEquipped;
    public bool HasJetPack { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        currFuel = maxFuel;

        tpp = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonPlayer>();
        inputManager = GetComponent<InputManager>();
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
                //player using true
                currFuel -= Time.deltaTime;
                effect1.Play();
                effect2.Play();
            }

            if (Input.GetKey(inputManager.useJetpack) || currFuel <= 0f)
            {
                //player using false
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
            yield return new WaitForSeconds(0.01f);
        }
    }
}
