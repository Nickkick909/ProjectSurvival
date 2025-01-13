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

    bool cursorLocked = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        cursorLocked = true;

        Vector3 savedPlayerPosition = new Vector3(PlayerPrefs.GetFloat("playerXPosition", 0), PlayerPrefs.GetFloat("playerYPosition", 1), PlayerPrefs.GetFloat("playerXPosition", 0));

        transform.position = savedPlayerPosition;
    }

    
    void Update()
    {
        HandleMovement();
        HandleLook();

        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (!cursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("playerXPosition", transform.position.x);
        PlayerPrefs.SetFloat("playerYPosition", transform.position.y);
        PlayerPrefs.SetFloat("playerZPosition", transform.position.z);
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
