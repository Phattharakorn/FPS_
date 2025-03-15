using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;
    public float lookSpeedX = 2f; // Speed of horizontal camera rotation
    public float lookSpeedY = 2f; // Speed of vertical camera rotation
    public float upperLookLimit = -80f; // Upper limit for camera pitch (looking up)
    public float lowerLookLimit = 80f; // Lower limit for camera pitch (looking down)

    private float currentSpeed;
    private bool isGrounded;
    private float verticalVelocity;

    private CharacterController characterController;
    private Transform cameraTransform;

    private float rotationX = 0f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Check if the CharacterController is attached to the GameObject
        if (characterController == null)
        {
            Debug.LogError("CharacterController is missing on the Player GameObject.");
        }

        cameraTransform = Camera.main.transform;

        // Lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (characterController == null) return; // Early exit if no CharacterController is found

        isGrounded = characterController.isGrounded;

        if (isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = 0f; // Reset vertical velocity when grounded
        }

        // Horizontal movement
        float moveDirectionX = Input.GetAxis("Horizontal");
        float moveDirectionZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveDirectionX + transform.forward * moveDirectionZ;

        // Determine the movement speed based on whether the player is running or walking
        currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        // Apply gravity
        verticalVelocity += gravity * Time.deltaTime;

        // Jumping
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Apply the movement
        characterController.Move(move * currentSpeed * Time.deltaTime);
        characterController.Move(Vector3.up * verticalVelocity * Time.deltaTime);

        // Look around
        LookAround();
    }

    void LookAround()
    {
        // Rotate the camera based on mouse movement
        float mouseX = Input.GetAxis("Mouse X") * lookSpeedX;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeedY;

        // Horizontal camera rotation
        transform.Rotate(Vector3.up * mouseX);

        // Vertical camera rotation (clamping the pitch)
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, upperLookLimit, lowerLookLimit);
        cameraTransform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
    }
}
