using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonPlayer : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform camera;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float speed;
    [SerializeField] private float jumpHeight = 6f;
    [SerializeField] private float turnSmoothTime = 0.1f;
    [SerializeField] public float gravity = -2.81f;

    
    public LayerMask groundMask;   
    public float groundDistance = 0.4f; 

    private float turnSmoothVelocity;
    private Vector3 velocity;
    private bool isGrounded;    

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0f)
        {
            velocity.y = -2f;  
        }

        Cursor.lockState = CursorLockMode.Locked;                   

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(horizontal, 0, vertical).normalized;

        if (dir.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(dir.x, dir.z)*Mathf.Rad2Deg + camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);             
        }
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity;

        controller.Move(velocity * Time.deltaTime);

    }

}
