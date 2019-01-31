using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;

    //Make sure run and temp are the same
    public float runSpeed = 40f;
    private float tempSpeed = 40f;
    private float defaultGravity = 4f;

    private float horizontalMove = 0f;
    private float timePressed = 0f;
    private bool jump = false;
    private bool crouch = false;
    private bool dash = false;
    private Rigidbody2D rb2d;

    //Values for dash
    private float dashTime;
    private float tempDashSpeed = 1f;
    public float startDashTime;
    public float dashSpeed;

    //Values for swing
    public Vector2 ropeHook;
    public bool isSwinging;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.bodyType = RigidbodyType2D.Dynamic;
    }

    // Update is called once per frame
    void Update(){
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        //Jump
        if (Input.GetButtonDown("Jump"))
            jump = true;

        //Dash
        if ((Input.GetButtonDown("Dash") || dash)){
            if (dashTime <= 0)
            {
                dash = false;
                dashTime = startDashTime;
                rb2d.velocity = Vector2.zero;
                tempDashSpeed = 1f;
            }
            else
            {
                jump = false;
                dash = true;
                tempDashSpeed = dashSpeed;
                dashTime -= Time.deltaTime;
            }
        }

        //While in the air
        if (!controller.m_Grounded){
            runSpeed = tempSpeed / 2f;
        }
        else{
            runSpeed = tempSpeed;
            rb2d.gravityScale = defaultGravity;
        }

        //Floating
        if (Input.GetButton("Float"))
            if(rb2d.velocity.y < -0.1f)
                rb2d.gravityScale = defaultGravity / 9f;
        if (Input.GetButtonUp("Float"))
            rb2d.gravityScale = defaultGravity;

        //Crouching
        if (Input.GetButtonDown("Crouch"))
            crouch = true;
        else if (Input.GetButtonUp("Crouch"))
            crouch = false;
    }

    private void FixedUpdate(){
        // Move Character
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump, tempDashSpeed);
        jump = false;
    }
}
