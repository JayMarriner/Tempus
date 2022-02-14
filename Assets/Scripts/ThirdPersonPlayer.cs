using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonPlayer : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform camera;   

    public Transform groundCheck;
    public LayerMask groundMask;
    [Range(-10, -1)]
    [SerializeField]public float gravity = -2.81f;
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
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        Cursor.lockState = CursorLockMode.Locked;                   

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(horizontal, 0, vertical).normalized;

        if (dir.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(dir.x, dir.y)*Mathf.Rad2Deg + camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);


            Vector3 moveDir = camera.TransformDirection(dir);
            velocity.y += gravity;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        if (controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            controller.Move(new Vector3(0, jumpHeight, 0));
        }
        Debug.Log(isGrounded);

        
    }

}
