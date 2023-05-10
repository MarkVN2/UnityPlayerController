using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Animator animator;                   // sets the animator as Animator, use this for animations in the code.

    [Header("Movement Settings")]
    
    public float moveSpeed = 60f;               // float for speed while moving 
    public float slideSpeed = 1.5f;             // float for speed multiplier while sliding
    public float jumpForce = 8.0f;              // 
    public float gravity = 20f;                 //

    bool isJumping = false;                          
    bool isDashing = false;                          
    bool isSliding = false;                         
    
    [Header("Camera Settings")]

    public Camera playerCamera;                 // sets the player camera as Camera

    public float lookSpeed = 2.0f;              // 
    public float lookXLimit = 45.0f;            // Limits X axis of the camera to a certain angle

    CharacterController charController;         //

    Vector3 moveDirection = Vector3.zero;       //
    float rotationX = 0;                        //

    [HideInInspector]
    public bool canMove = true;                 //
    bool inAir;                                 // 
    private GameObject capsule;

    private void Start()
    {
        charController = GetComponent<CharacterController>();       
        
        Cursor.lockState = CursorLockMode.Locked;                 
        Cursor.visible = false;                                    
    }
    private void Update()
    {
        bool isCrouching = Input.GetKey(KeyCode.LeftControl);
        bool isSliding = input.GetKey(KeyCode.LeftControl);

        Vector3 forward = transform.transformDirection(Vector3.forward);
        Vector3 right  = transform.transformDirection(Vector3.right);
        
        float curSpeedX = canMove ? (isSliding ? moveSpeed * slideSpeed : moveSpeed) * input.GetAxis("Vertical"): 0;
        float curSpeedY = canMove ? (isSliding ? moveSpeed * slideSpeed : moveSpeed) * input.GetAxis("Horizontal"): 0;

        float movementDirectionY = moveDirection.y;

        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (charController.isGrounded){            
            if(Input.GetButton("Jump")){
                moveDirection.y  = jumpForce;
            }
        }

        movementDirectionY  -= gravity  * Time.deltaTime;
        moveDirection.y = movementDirectionY;
        
        //Camera rotation   
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;                                     //

            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);                            //

            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);               //

            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);     //
        }
    }
}
