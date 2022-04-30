using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatanaHandler : MonoBehaviour
{
    [SerializeField] Transform initPos;
    [SerializeField] Transform startSwing;
    [SerializeField] Transform endSwing;
    [SerializeField] GameObject katanaObj;
    [Range(1,10)]
    [SerializeField] float powerMultiplier;
    [SerializeField] Animator playerAnimator;
    [SerializeField] TrailRenderer trail;
    InputManager inputManager;
    Quaternion initRot;

    public bool swinging;
    public bool slashing;

    // Start is called before the first frame update
    void Start()
    {
        inputManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<InputManager>();
        initRot = transform.rotation;
        trail.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(inputManager.shoot) && swinging && !slashing)
        {
            playerAnimator.SetBool("Slash", true);
            slashing = true;
            StartCoroutine(SlashCoolDown(0.8f));
        }

        if (Input.GetKey(inputManager.shoot) && !swinging && !slashing)
        {
            playerAnimator.SetTrigger("Swing");
            swinging = true;
            StartCoroutine(SwingCoolDown(0.8f));
        }

        /*
        if (Input.GetKey(inputManager.aim) && !Input.GetKey(inputManager.shoot))
        {
            katanaObj.transform.position = Vector3.Lerp(katanaObj.transform.position, startSwing.position, 1.10F * Time.deltaTime * powerMultiplier);
            //transform.rotation = Quaternion.Euler(-45, 0, 0);
            //transform.rotation = Quaternion.Lerp(Quaternion.Euler(0,0,0), Quaternion.Euler(-45,0,0), 1.10f * Time.deltaTime * powerMultiplier);
        }
        else if (Input.GetKey(inputManager.aim) && Input.GetKey(inputManager.shoot))
        {
            katanaObj.transform.position = Vector3.Lerp(katanaObj.transform.position, endSwing.position, 1.1f * Time.deltaTime * powerMultiplier * 10);
            //transform.rotation = Quaternion.Lerp(Quaternion.Euler(-45, 0, 0), Quaternion.Euler(45, 0, 0), 1.10f * Time.deltaTime * powerMultiplier * 10);
        }
        else if (Input.GetKey(inputManager.shoot) && !Input.GetKey(inputManager.aim))
        {
            katanaObj.transform.position = Vector3.Lerp(katanaObj.transform.position, endSwing.position, 1.1f * Time.deltaTime * powerMultiplier * 10);
            //transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, 0, 0), Quaternion.Euler(45, 0, 0), 1.10f * Time.deltaTime * powerMultiplier * 10);
        }
        else
        {
            katanaObj.transform.position = Vector3.Lerp(katanaObj.transform.position, initPos.position, 1.1f * Time.deltaTime * powerMultiplier);
            //transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        */
    }

    public void SpecialAttack()
    {
        playerAnimator.SetBool("Special", true);
        StartCoroutine(SpecialCoolDown());
    }

    IEnumerator SwingCoolDown(float timeAmt)
    {
        trail.enabled = true;
        trail.Clear();
        slashing = true;
        yield return new WaitForSeconds(0.1f);
        slashing = false;
        yield return new WaitForSeconds(timeAmt);
        swinging = false;
        yield return new WaitForSeconds(0.25f);
        if(!slashing)
            trail.enabled = false;
    }


    IEnumerator SlashCoolDown(float timeAmt)
    {
        trail.enabled = true;
        trail.Clear();
        yield return new WaitForSeconds(timeAmt);
        slashing = false;
        playerAnimator.SetBool("Slash", false);
        yield return new WaitForSeconds(0.25f);
        trail.enabled = false;
    }

    IEnumerator SpecialCoolDown()
    {
        trail.enabled = true;
        trail.Clear();
        GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonPlayer>().stopMovement = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonPlayer>().specialHit = true;
        yield return new WaitForSeconds(2f);
        GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonPlayer>().stopMovement = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonPlayer>().specialHit = false;
        playerAnimator.SetBool("Special", false);
        yield return new WaitForSeconds(0.25f);
        trail.enabled = false;
    }
}
