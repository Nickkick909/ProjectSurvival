using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float playerSpeed = 5.0f;
    [SerializeField] bool isGrounded = true;
    [SerializeField] float mouseSpeed = 5f;
    [SerializeField] float jumpHeight = 5f;
    [SerializeField] Transform playerCamera;
    [SerializeField] float interactRange = 5f;
    [SerializeField] LayerMask interactMask;

    [SerializeField] KeyCode interactKey;

    float gravityValue = -9.81f;
    Vector3 playerVelocity;

    CharacterController controller;
    float xRotation;
    

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    
    void Update()
    {
        HandleMovement();
        HandleLook();
    }

    void HandleMovement()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerVelocity.y = jumpHeight;

            }

        }

        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector3 move = (transform.right * horizontalInput) + (transform.forward * verticalInput);
        controller.Move(playerSpeed * Time.deltaTime * move.normalized);


        if (!isGrounded)
        {
            playerVelocity.y += gravityValue * Time.deltaTime;
        }


        controller.Move(playerVelocity * Time.deltaTime);

        
    }

    void HandleLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSpeed * Time.deltaTime;

        transform.Rotate(Vector3.up, mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    
}
