using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //references
    public CharacterController2D controller;
    public Animator animator;
    public Rigidbody2D rb;
    public BoxCollider2D smallCollider;
    public ViewSwitching viewSwitching;
    public ParticleSystem dustTrail;
    public ParticleSystem dustImpact;
	ParticleSystem.EmissionModule dustEmission;
    AudioManager audioManager;
    GameManager gameManager;


    //player costumization
    public float climbSpeed = 30f;
    public float runSpeed = 40f;
    public float footstepsSpeed = .3f;
    [HideInInspector]
    public bool bounced;
    [HideInInspector]
    public bool collOff = false;
    
    //variables
    float horizontalMove = 0f;
    float verticalMove = 0f;
    bool jump = false;
    bool climbing = false;

    private void Awake() 
    {
        audioManager = FindObjectOfType<AudioManager>();
        gameManager = FindObjectOfType<GameManager>();
        dustEmission = dustTrail.emission;
    }

    private void Update() 
    {
        if(viewSwitching.onLayout == false) //if not on layout set player speed and enable running animation
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

            if(horizontalMove != 0f && controller.m_Grounded) //dust particles
            {
                dustEmission.rateOverTime = 50f;
            }
            else
            {
                dustEmission.rateOverTime = 0f;
            }
        }
        else //disable animation and dust particles on layout
        {
            animator.SetFloat("Speed", 0);
            dustEmission.rateOverTime = 0f;
        }
           
        if(Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }
    
    private void FixedUpdate() 
    {
        if(viewSwitching.onLayout == false) //move player if not on layout
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
        }
        else //set player's speed to 0 if on layout
        {
            controller.Move(0, false);
        }

        jump = false;

        if(climbing)
        {
            if(viewSwitching.onLayout == false) //if not on layout, move player up and down
            {
                verticalMove = Input.GetAxisRaw("Vertical");
                rb.velocity = new Vector2(rb.velocity.x, verticalMove * climbSpeed * Time.fixedDeltaTime);
            }
            rb.gravityScale = 0f;
        }
        else
        {
            rb.gravityScale = 3.5f;
        }
    }

    public void Slime(float slimeForce)
    {
        rb.velocity = new Vector2(rb.velocity.x, slimeForce);
        audioManager.Play("SlimeBounce");

        controller.coyoteTimeCounter = 0f;
		controller.jumpBufferCounter = 0f;
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Spike")
        {
            gameManager.playerIsDead = true;
        }
    }

    //climbing
    private void OnTriggerEnter2D(Collider2D other) 
    {
        //Ladder
        if(other.gameObject.tag == "Ladder")
        {
            if(Input.GetKey(KeyCode.W))
            {
                climbing = true;
                animator.SetBool("Climbing", true);
            }
        }

        if(other.gameObject.tag == "Dripstone")
        {
            gameManager.playerIsDead = true;
        }

        if(other.gameObject.tag == "collTrigger")
        {
            if(collOff == false)
            {
                smallCollider.enabled = false;
                collOff = true;
            }
            else
            {
                smallCollider.enabled = false;
                collOff = false;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Ladder")
        {
            if(Input.GetKey(KeyCode.W))
            {
                climbing = true;
                animator.SetBool("Climbing", true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Ladder")
        {
            climbing = false;
            animator.SetBool("Climbing", false);
        }
    }

    //dust impact on landing
    public void ImpactDust()
    {
        dustImpact.gameObject.SetActive(true);
    }
}