using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Animator animator;                   // sets the animator as Animator, use this for animations in the code.

    [Header("Movement Settings")]
    
    public float runSpeed = 60f;                // float for speed while moving 
    public float crouchSpeed = 40f;             // float for speed while crouching
    public float slideSpeed = 70f;              // float for speed while sliding
    public float jumpForce = 8.0f;              // float for force when jumping 
    public float gravity = 20f;                 //

    bool jump = false;                          // boolean for jumping
    bool crouch = false;                        // boolean for crouching    
    bool dash = false;                          // boolean for dashing
    bool sliding = false;                       // boolean for sliding
    
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

    private void Start()
    {
 
        charController = GetComponent<CharacterController>();       

        Cursor.lockState = CursorLockMode.Locked;                   //Locks cursor
        Cursor.visible = false;                                     //Hides Cursor
    }
    private void Update()
    {
        bool isSliding = Input.GetKey(KeyCode.LeftControl);

        if (charController.isGrounded){            
            if(Input.GetButton("Jump")){
                moveDirection.y  = jumpForce;
            }
        }

        Debug.Log(charController.isGrounded);
        
       if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;                                 //

            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);                        //

            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);           //

            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0); //
        }
    }
}
