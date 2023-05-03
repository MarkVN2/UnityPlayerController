using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;    // 
    public Animator animator;                   // sets the animator as Animator, use this for animations in the code.
    public float runSpeed = 30f;                // speed of character while moving
    bool jump = false;                          // boolean for jumping
    bool crouch = false;                        // boolean for crouching    
    float horizontalMove = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
          
        if(Input.GetButtonDown("Jump")){
           animator.SetBool("isJumping", true);
           jump = true;
        }
        if(Input.GetButtonDown("Crouch")){
            animator.SetBool("isCrouching", true);
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch")){
            animator.SetBool("isCrouching", false);
            crouch = false;
        }
    }

    public void OnLanding(){
        animator.SetBool("IsJumping", false);  
     }
     void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch , jump);
        jump = false;
        
    }
}
