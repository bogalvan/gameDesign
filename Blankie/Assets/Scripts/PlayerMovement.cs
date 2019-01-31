using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public enum PlayerState {idle, running, dashing, jumping, crawling, floating, grappling }
    public CircleCollider2D playerCollider;
    //Animation 
    private Animator anim;
    public PlayerState playerState = PlayerState.running;

    //Make sure run and temp are the same
    public float runSpeed = 40f;
    private float tempSpeed = 40f;
    private float defaultGravity = 4f;

    private float horizontalMove = 0f;
    private float timePressed = 0f;
    private bool jump = false;
    private bool crouch = false;
    private bool dash = false;
    private bool grappling = false;
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
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.bodyType = RigidbodyType2D.Dynamic;
        playerState = PlayerState.running;
    }

    // Update is called once per frame
    void Update(){
        playerState = PlayerState.running;
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        //Jump
        if (Input.GetButtonDown("Jump")) 
        {
            jump = true;
        } 

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
        {
            //print("float");
            playerState = PlayerState.floating;
            if (rb2d.velocity.y < -0.1f)
                rb2d.gravityScale = defaultGravity / 9f;
        }
        if (Input.GetButtonUp("Float"))
        {
            //print("float");
            playerState = PlayerState.floating;
            rb2d.gravityScale = defaultGravity;
        }

        //Crouching
        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }

        //Set states 
        if(crouch == true)
        {
            playerState = PlayerState.crawling;
            playerCollider.radius = .3f;
        }
        else
        {
            playerCollider.radius = .72f;
        }
        if (dash == true)
        {
            playerState = PlayerState.dashing;
        }
        if (grappling == true)
        {
            playerState = PlayerState.grappling;
        }

        if (jump == true && playerState != PlayerState.floating)
        {
            //print("jump");
            playerState = PlayerState.jumping;
        }
        //print(playerState);
        switch (playerState)
        {
            case PlayerState.running:
                anim.SetTrigger("run");
                break;
            case PlayerState.crawling:
                anim.SetTrigger("crawl");
                break;
            case PlayerState.dashing:
                anim.SetTrigger("dash");
                break;
            case PlayerState.jumping:
                anim.SetTrigger("jump");
                break;
            case PlayerState.grappling:
                anim.SetTrigger("grapple"); 
                break;
            case PlayerState.floating:
                anim.SetTrigger("floatInAir");
                break;
            default:
                anim.SetTrigger("run");
                break;
        }



    }

    public void isGrappling(bool isGrappling)
    {
        //print(isGrappling);
        if (isGrappling)
        {
            anim.SetTrigger("grapple");
            playerState = PlayerState.grappling;
        }
        grappling = isGrappling;
    }

    private void FixedUpdate(){
        // Move Character
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump, tempDashSpeed);
        jump = false;
    }
}
