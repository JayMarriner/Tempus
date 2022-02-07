using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonPlayer : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform camera;

    public Transform groundCheck;
    public LayerMask groundMask;
    public float gravity = -9.81f;
    public float groundDistance = 0.2f;

    [SerializeField] private float speed = 6f;
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private float turnSmoothTime = 0.1f;

    private float turnSmoothVelocity;
    private Vector3 velocity;
    private bool isGrounded;

    // Update is called once per frame
    void Update()
    {
        Debug.Log(isGrounded);

        Cursor.lockState = CursorLockMode.Locked;

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; 
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(horizontal, 0, vertical).normalized;

        if (dir.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(dir.x, dir.y)*Mathf.Rad2Deg + camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);


            Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * 2.0f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
