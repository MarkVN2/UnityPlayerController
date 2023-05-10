
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{

    //TODO
    /* 
        Create Speed multiplier while in the air;
        Create Dash;
        Make collider and camera go down if Sliding;
    */

    public Animator animator;                   // sets the animator as Animator, use this for animations in the code.

    [Header("Movement Settings")]
    
    public float moveSpeed = 45f;               // float for speed while moving 
    public float slideSpeed = 1.5f;             // float for speed multiplier while sliding
    public float jumpForce = 13f;               // 
    public float gravity = 20f;                 //
    public float maxSpeedMultiplier = 2f;       // NOT IN USE FOR NOW

    float multiplier = 1f;
    bool isJumping;                          
    bool isDashing;                          
    bool isSliding;                         

    [Header("Camera Settings")]

    public Camera playerCamera;                 // sets the player camera as Camera
    public float lookSpeed = 2.0f;              // 
    public float lookXLimit = 90.0f;            // Limits axis of the camera to a certain angle
    CharacterController charController;         //
    Vector3 moveDirection = Vector3.zero;       //
    float rotationX = 0;                        //

    [HideInInspector]
    public bool canMove = true;                 //
    bool inAir;                                 // 

    private void Start()
    {
        charController = GetComponent<CharacterController>();       
        
        Cursor.lockState = CursorLockMode.Locked;                 
        Cursor.visible = false;                          

    }
    private void Update()
    {
        
        bool inAir = (charController.isGrounded) ? false : true;

        bool isSliding = Input.GetKey(KeyCode.LeftControl);

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right  = transform.TransformDirection(Vector3.right);
        
        float curSpeedX = canMove ?  moveSpeed  * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ?  moveSpeed  * Input.GetAxis("Horizontal") : 0;

        float movementDirectionY = moveDirection.y;

        moveDirection = (forward * (curSpeedX + (isSliding ? slideSpeed : 0)) ) + (right * curSpeedY ) ;

        movementDirectionY  -= gravity  * Time.deltaTime;
        moveDirection.y = movementDirectionY;
        
        charController.Move(moveDirection * Time.deltaTime * multiplier);
        
        Debug.Log("Grounded:" + (charController.isGrounded));
        Debug.Log("In Air:" + (inAir));

        //Camera rotation   
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;                                     //

            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);                            //

            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);               //

            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);     //
        }
    }
     private void FixedUpdate()
        {
        if (charController.isGrounded){ 
            if(Input.GetButton("Jump")){
                moveDirection.y  = jumpForce;
            }
          }
        }

}

