using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public CharacterController controller;
    public float speed;
    public float crouchSpeed;       // Slower speed while crouching
    private float gravity = -20f;
    private float jumpHeight = 1f;

    public Transform groundCheck;
    private float groundDistance = 0.4f;
    public LayerMask groundMask;

    // Standing/crouching settings
    public float standHeight = 1.5f;
    public float crouchHeight = .75f;

    private Vector3 velocity;
    private bool isGrounded;
    private bool isCrouching = false;
    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //local movement calculation
        Vector3 move = transform.right * x + transform.forward * z;
        
        float currentSpeed = isCrouching ? crouchSpeed : speed;
        controller.Move(move * currentSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            ToggleCrouch();
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void ToggleCrouch()
    {
        // Toggle the isCrouching boolean
        isCrouching = !isCrouching;
        if (isCrouching)
        {
            controller.height = crouchHeight;
        }
        else
        {
            // (Optional) If you want to check overhead clearance before standing, 
            // you would do a raycast above the player here to ensure it’s safe.
            controller.height = standHeight;
        }
    }


}
