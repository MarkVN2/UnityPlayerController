using UnityEngine;

public class Player : MonoBehaviour
{

    public Animator animator;                   // sets the animator as Animator, use this for animations in the code.
    public Camera camera;                       // sets camera as Camera

    
    public float runSpeed = 60f;                // float for speed while moving 
    public float crouchSpeed = 40f;             // float for speed while crouching
    public float slideSpeed = 70f;              // float for speed while sliding
    public float jumpForce = 80f;               // float for force when jumping 
                 

    bool jump = false;                          // boolean for jumping
    bool crouch = false;                        // boolean for crouching    
    bool dash = false;                          // boolean for dashing
    bool sliding = false;                       // boolean for sliding
    
    private void Start()
    {
        
    }
    private void Update()
    {
        
    }
}

public class dash(){
    
}