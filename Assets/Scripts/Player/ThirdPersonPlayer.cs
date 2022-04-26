using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class ThirdPersonPlayer : MonoBehaviour
{
    [Header("Shoulder cam settings.")]
    [SerializeField] CinemachineVirtualCamera shoulderCamera;
    [Range(1,100)]
    [SerializeField] int shoulderHorSpeed;
    [Range(1,100)]
    [SerializeField] int shoulderVerSpeed;

    [Header("Player settings.")]
    [Range(1, 10)]
    [SerializeField] private float jumpForce = 5f;
    [Range(1, 10)]
    [SerializeField] private float speed = 5f;
    [Range(1, 20)]
    [SerializeField]private float gravity = 9f;

    [Header("HealthBar")]
    public float currHealth, maxHealth=10f;
    
    [Header("Animation settings.")]
    [SerializeField] Animator anim;

    [Header("Healthbar")]
    [SerializeField] Image healthBar;
    [SerializeField] GameObject healthObject;
    bool healthChanging;

    InputManager inputManager;
    CharacterController controller;
    Transform cam;
    Vector3 vertVel;
    float rotationSmooth = 0.1f;
    float turnSmoothVelocity;
    public bool stopMovement;
    bool falling;
    bool running;
    bool jogging;
    bool jumped;
    bool fallTimer;

    [Header("Bool settings.")]
    public bool shoulderView;
    public bool stopCamMove;

    private void Start()
    {
        inputManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<InputManager>();
        healthObject.SetActive(false);
        controller = gameObject.GetComponent<CharacterController>();
        cam = Camera.main.transform;
        currHealth = maxHealth;
    }

    void Update()
    {
        //Testing the health bar with damage. (didnt add this to input manager because it is a temp measure).
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(2.5f);
        }         
        //Set shoulder camera to higher priority.
        if (Input.GetKeyDown(inputManager.aim))
        {
            shoulderView = true;
            shoulderCamera.Priority = 11;
        }
        //Puts priority of shoulder camera down to let main camera take over.
        else if (Input.GetKeyUp(inputManager.aim) || stopCamMove)
        {
            shoulderView = false;
            shoulderCamera.Priority = 1;
        }

        //If the player is greater than x amount off the floor then it will play the falling animation.
        if(!FallGrounded())
        {
            anim.SetBool("Falling", true);
            falling = true;
        }

        //Fail safe to knock off the falling animation when the player has reached the ground.
        if(IsGrounded() && falling)
        {
            anim.SetBool("Falling", false);
            falling = false;
        }

        Jump();
        Movement();
    }

    void Movement()
    {
        //Kill switch for movement, useful for menus, etc.
        if (stopMovement)
            return;

        //Store input as vector.
        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        int sprintSpeed;
        //Allow for the player to sprint using key input.
        if (Input.GetKey(inputManager.sprint))
        {
            sprintSpeed = 2;
            running = true;
            anim.SetBool("Run", true);
        }
        //else set the speed back to base.
        else
            sprintSpeed = 1;

        //Fail safe, if the player stops pressing sprint button then setbool back to false and turn anim off.
        if(Input.GetKeyUp(inputManager.sprint) && running)
        {
            running = false;
            anim.SetBool("Run", false);
        }

        //Fail safe, if the bool is left on somehow but the player isn't moving it will turn the animation off.
        if (movement.magnitude == 0 && running)
        {
            anim.SetBool("Run", false);
        }

        //If the player is moving but not running then jog animation will play.
        if (movement.magnitude >= 0.1f && !running)
        {
            anim.SetBool("Jog", true);
            jogging = true;
        }
        //Else turn jogging off.
        else
        {
            anim.SetBool("Jog", false);
            jogging = false;
        }

        //If not in the shoulder view then...
        if (!shoulderView)
        {
            //If player is moving then...
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
                controller.Move(moveDirection * speed * sprintSpeed * Time.deltaTime);
            }
        }
        else
        {
            //If player is moving then...
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
                controller.Move(moveDirection * speed * sprintSpeed * Time.deltaTime);
            }
            //Allow rotation on the shoulder view. There is a serializeField for changing this value to lower or increase sensitivity.
            gameObject.transform.Rotate(0, Input.GetAxis("Mouse X") * Time.deltaTime * shoulderHorSpeed, 0);

            //Setting the camera offset.
            var shoulderOffset = shoulderCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y;
            shoulderCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y += -Input.GetAxis("Mouse Y") * Time.deltaTime * shoulderVerSpeed;
            if (shoulderOffset > 2)
                shoulderCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y = 2;
            else if (shoulderOffset < -2)
                shoulderCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y = -2;
        }
    }

    void Jump()
    {
        //Apply jump.
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            if (running)
                vertVel.y = jumpForce;
            else
                vertVel.y = jumpForce*0.8f;
            anim.SetTrigger("Jump");
            StartCoroutine(FallRollTimer());
        }
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
        return (Physics.Raycast(ray, 0.5f));
    }

    bool FallGrounded()
    {
        //Send ray down from player.
        Ray ray = new Ray(new Vector3(controller.bounds.center.x, (controller.bounds.center.y - controller.bounds.extents.y), controller.bounds.center.z), Vector3.down);
        //Return if on ground (with degree of inaccuracy)
        return (Physics.Raycast(ray, 3f));
    }

    public void TakeDamage(float amt)
    {
        //Deduct the health based on a random value.
        currHealth -= amt;
        anim.SetTrigger("Hurt");
        StartCoroutine(HealthTarget());
    }

    IEnumerator FallRollTimer()
    {
        if (fallTimer)
        {
            anim.SetBool("FallRoll", false);
            anim.SetBool("HardLand", false);
            yield break;
        }
        fallTimer = true;
        //Change this to change length of time in air needed to apply landing anims.
        yield return new WaitForSeconds(1.5f);
        //Check if player has reached the floor or not, if they havent the landing anims will be applied.
        if (!IsGrounded())
        {
            //Waits until the player has reached the floor whilst constantly checking what buttons the player is pressing - this stops the check from accidentally doing wrong anim.
            while (!IsGrounded())
            {
                if (running)
                    anim.SetBool("FallRoll", true);
                else
                    anim.SetBool("FallRoll", false);
                if (!running)
                    anim.SetBool("HardLand", true);
                else
                    anim.SetBool("HardLand", false);
                yield return new WaitForSeconds(0.1f);
            }
            //If the player isn't running then play the hard landing animation.
            if (!running)
            {
                anim.SetBool("HardLand", true);
                stopMovement = true;
                yield return new WaitForSeconds(1f);
                anim.SetBool("HardLand", false);
                stopMovement = false;
            }
            //If the player was running then the roll animation will play.
            else
            {
                anim.SetBool("FallRoll", true);
                stopMovement = true;
                yield return new WaitForSeconds(0.5f);
                anim.SetBool("FallRoll", false);
                stopMovement = false;
            }
        }
        fallTimer = false;
    }

    IEnumerator HealthTarget()
    {
        healthChanging = true;
        healthObject.SetActive(true);
        while(healthBar.fillAmount > currHealth / 10)
        {
            healthBar.fillAmount -= 0.01f;
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(1f);
        healthObject.SetActive(false);
        healthChanging = false;
    }
}