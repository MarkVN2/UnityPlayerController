
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour{

    
    /* 
      TODO Create Dash;
    */

    public Animator animator;                   // sets the animator as Animator, use this for animations in the code.

    [Header("Movement Settings")]
    
    public float moveSpeed = 45f;               // float for speed while moving 
    public float slideSpeed = 1.5f;             // float for  the extra speed while sliding
    public float jumpForce = 13f;               

    public float dashForce = 2f;
    public float gravity = 20f;                 
    public float maxSpeedMultiplier = 2f;       

    public float airAcceleration = 0.2f;

    float multiplier = 1f;

    float dashCd = 2;
    float nextDash;
    
    bool isJumping;                          
    bool isDashing;        
    bool ableDash;                  
    bool isSliding;                         

    [Header("Camera Settings")]

    public Camera playerCamera;               
    public float lookSpeed = 2.0f;              
    public float lookXLimit = 90.0f;           
    CharacterController charController;         
    Vector3 moveDirection = Vector3.zero;       
    float rotationX = 0;                        

    [HideInInspector]
    public bool canMove = true;                 
    bool inAir;         
    Transform tr;                         


    private void Start(){
        charController = GetComponent<CharacterController>();       
        tr = GetComponent<Transform>();

        Cursor.lockState = CursorLockMode.Locked;                 
        Cursor.visible = false;                          
    }
    private void Update(){
        
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right  = transform.TransformDirection(Vector3.right);

        //Slide
            bool isSliding = Input.GetKey(KeyCode.LeftControl);

    
            //Setting raycast
            
            Vector3 top = tr.position + new Vector3(0,0.65f,0); // Character Top

            Ray upRay = new Ray(top, new Vector3(0,1f,0));
            RaycastHit hit;

            if (isSliding){
                transform.localScale = new Vector3(1, 0.5f,1);
            }
            else{
                if (Physics.Raycast(upRay, out hit, 0.25f) ){ 
                    transform.localScale = new Vector3(1, 0.5f,1);

                    Debug.DrawRay(upRay.origin, upRay.direction * hit.distance , Color.red);
                    print("Hit");
            
                }
                else{
                    transform.localScale = new Vector3(1,1,1);
                    
                    Debug.DrawRay(upRay.origin, upRay.direction  , Color.blue);
                    print("Not Hit");
                }

 

            }

        // Air Acceleration
            bool inAir = (charController.isGrounded) ? false : true;

            if (inAir == true ){

                if(maxSpeedMultiplier > multiplier){
                    multiplier += airAcceleration * Time.deltaTime ; 
                }
                else{
                    multiplier = maxSpeedMultiplier;
                }
            }
            else{
            if (multiplier > 1){
            multiplier -= airAcceleration * Time.deltaTime;
            }
            else{
                multiplier = 1f;
            }
        }
    
        
        //Current Speed

            float curSpeedX = canMove ?  moveSpeed  * multiplier * Input.GetAxis("Vertical") : 0;
            float curSpeedY = canMove ?  moveSpeed  * multiplier * Input.GetAxis("Horizontal") : 0;

            float movementDirectionY = moveDirection.y;

        //Move Value
            moveDirection = (forward *  (curSpeedX + (isSliding ? slideSpeed : 0 )) ) + (right * (curSpeedY + (isSliding ? slideSpeed : 0))) ;
            
            //Gravity
            movementDirectionY  -= gravity  * Time.deltaTime;
            moveDirection.y = movementDirectionY;
        
        //Move character

            charController.Move(moveDirection * Time.deltaTime);
        
        //Dash

            //Dash Cooldown
            if (Time.time > nextDash){ 

                if (isDashing = Input.GetKey(KeyCode.LeftShift)){

                print("Dash used");

                nextDash = Time.time  + dashCd;

                }
            }

            if (isDashing){
            string nothingisworkinghere = "nothing";  // !FIX THIS!
            } 



        //Logs
    
        //Camera rotation   
        if (canMove){
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;                                     

            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);                            

            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);               

            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);     
        }

        
    }
    private void FixedUpdate(){
        if (charController.isGrounded){ 
            if(Input.GetButton("Jump")){
                moveDirection.y  = jumpForce;
            }
          }
        }
}