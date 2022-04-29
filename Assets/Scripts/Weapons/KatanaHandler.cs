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
    InputManager inputManager;
    float initRot;

    // Start is called before the first frame update
    void Start()
    {
        inputManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<InputManager>();
        initRot = transform.eulerAngles.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(inputManager.aim) && !Input.GetKey(inputManager.shoot))
        {
            katanaObj.transform.position = Vector3.Lerp(katanaObj.transform.position, startSwing.position, 1.10F * Time.deltaTime * powerMultiplier);
            katanaObj.transform.rotation = Quaternion.Lerp(katanaObj.transform.rotation, Quaternion(katanaObj.transform.rotation.x + 10, katanaObj.transform.rotation.y, katanaObj.transform.rotation.z), 1.1f * Time.deltaTime * powerMultiplier * 5);
        }
        else if(Input.GetKey(inputManager.aim) && Input.GetKey(inputManager.shoot))
        {
            katanaObj.transform.position = Vector3.Lerp(katanaObj.transform.position, endSwing.position, 1.1f * Time.deltaTime * powerMultiplier * 10);
        }
        else
        {
            katanaObj.transform.position = Vector3.Lerp(katanaObj.transform.position, initPos.position, 1.1f * Time.deltaTime * powerMultiplier);
        }

    }
}
