using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ThirdPersonPlayer : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera shoulderCamera;
    [Range(1, 10)]
    [SerializeField] private float jumpForce = 5f;
    [Range(1, 10)]
    [SerializeField] private float speed = 5f;
    [Range(1, 20)]
    [SerializeField]private float gravity = 9f;

    InputManager inputManager;
    CharacterController controller;
    Transform cam;
    Vector3 vertVel;
    float rotationSmooth = 0.1f;
    float turnSmoothVelocity;
    bool shoulderView;
    public bool stopCamMove;

    private void Start()
    {
        inputManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<InputManager>();
        controller = gameObject.GetComponent<CharacterController>();
        cam = Camera.main.transform;
    }

    void Update()
    {
        if (Input.GetKeyDown(inputManager.aim))
        {
            shoulderView = true;
            shoulderCamera.Priority = 11;
        }
        else if (Input.GetKeyUp(inputManager.aim) || stopCamMove)
        {
            shoulderView = false;
            shoulderCamera.Priority = 1;
        }

        Jump();
        Movement();
    }

    void Movement()
    {
        //Store input as vector.
        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (!shoulderView)
        {
            if (movement.magnitude >= 0.1f)
            {
                //Determine rotation angle
                float targetAngle = Mathf.Atan2(movement.x, movement.y) * Mathf.Rad2Deg + cam.eulerAngles.y;

                //Smooth between current and target angle
                float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, rotationSmooth);

                //Apply rotation to the transform
                transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);

                //If direction magnitude greater than 1 normalise it
                if (movement.sqrMagnitude > 1)
                    movement.Normalize();

                //Movement with normalised
                Vector3 moveDirection = (Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward) * movement.sqrMagnitude;

                //Apply movement
                controller.Move(moveDirection * speed * Time.deltaTime);
            }
        }
        else
        {
            if(movement.magnitude >= 0.1f)
            {
                //Determine rotation angle
                float targetAngle = Mathf.Atan2(movement.x, movement.y) * Mathf.Rad2Deg + cam.eulerAngles.y;

                //If direction magnitude greater than 1 normalise it
                if (movement.sqrMagnitude > 1)
                    movement.Normalize();

                //Movement with normalised
                Vector3 moveDirection = (Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward) * movement.sqrMagnitude;

                //Apply movement
                controller.Move(moveDirection * speed * Time.deltaTime);
            }
            gameObject.transform.Rotate(0, Input.GetAxis("Mouse X"), 0);
        }
    }

    void Jump()
    {
        //Apply jump.
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
            vertVel.y = jumpForce;
        //Else apply gravity.
        else
            vertVel.y -= gravity * Time.deltaTime;

        //Apply movement.
        controller.Move(vertVel * Time.deltaTime);
    }

    bool IsGrounded()
    {
        //Send ray down from player.
        Ray ray = new Ray(new Vector3(controller.bounds.center.x, (controller.bounds.center.y - controller.bounds.extents.y), controller.bounds.center.z), Vector3.down);
        //Return if on ground (with degree of inaccuracy)
        return (Physics.Raycast(ray, 0.3f));
    }
}