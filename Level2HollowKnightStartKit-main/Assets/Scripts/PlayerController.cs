using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script NEEDS a mover and a jumper to work. This will automatically add them if there isn't one of each
[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Jumper))]

public class PlayerController : MonoBehaviour


{
    [Tooltip("How well we can control ourselves in the air. 1 = same as on ground")]
    public float airControl = 0.5f;

    //these are all just references to the various components attached to this object to make
    //our lives easier
    private Mover mover;
    private Jumper jumper;
    private Animator animator;
    private Dasher dasher;

    //2-13-26 start
    public bool canMove = true;
    //2-13-26 end
    public ProjectileShooter projectileShooter1;
    public ProjectileShooter projectileShooter2;
    //adjustment to fix flipping issue
    public SpriteRenderer spriteRenderer; // drag your player's sprite here in the Inspector

    void Start()
    {
        //Find all the componenets attached to this object and save them to references
        mover = gameObject.GetComponent<Mover>();
        jumper = gameObject.GetComponent<Jumper>();
        animator = gameObject.GetComponent<Animator>();
        dasher = gameObject.GetComponent<Dasher>();

        // Auto-find sprite renderer if not assigned
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer == null)
                spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        //If we have a projectile shooter, we need to set it facing the right direction
      //  if (projectileShooter1 != null)
        {
      //      projectileShooter1.SetDirection(new Vector2(1, 0));
        }
        if (dasher != null)
        {
            dasher.SetDirection(new Vector2(1f, 0f));
        }


        //temporarily turning off projectileshooter2:
        projectileShooter2 = null;


        if (projectileShooter2 != null)
        {
            projectileShooter2.SetDirection(new Vector2(1, 0));
        }
    }

    // Update is called once per frame
    void Update()
    {

        //2-13-26 start
        if (!canMove)
            return;
        //2-13-26 end


        //If we have an animator...
        if (animator != null)
        {
            //Tell the animator that we are not currently walking 
            animator.SetBool("Walking", false);
            //Tell the animator whether or not we're in the air
            animator.SetBool("IsOnGround", jumper.GetIsOnGround());
            //Tell the animator our current y velocity 
      animator.SetFloat("YVelocity", GetComponent<Rigidbody2D>().linearVelocity.y);
            //It uses all these things to decide which animation to play
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        //Debug.Log("Horizontal = " + horizontal);

        // Handle visual facing direction
        if (horizontal > 0)
            transform.rotation = Quaternion.Euler(transform.rotation.x, 0f, transform.rotation.z);
        else if (horizontal < 0)
            transform.rotation = Quaternion.Euler(transform.rotation.x, 180f, transform.rotation.z);

        //Ask the jumper if we're in the air. If we are, apply the air control modifier
        float airControlModifier = jumper.GetIsOnGround() ? 1f : airControl;

        // Apply movement
        if (horizontal != 0)
        {
            mover.AccelerateInDirection(new Vector2(horizontal * airControlModifier, 0f));

            if (animator != null)
                animator.SetBool("Walking", true);

            // Set projectile direction based on facing
            if (projectileShooter1 != null)
                projectileShooter1.SetDirection(new Vector2(horizontal, 0.1f));
        }

        if (dasher != null)
        {
            dasher.SetDirection(new Vector2(-1f, 0f));
        }


        //When Jumping
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        {
            //If the jump key is pressed... jump!
            jumper.Jump();
        }

        //When Dashing
        if( Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            if (dasher != null)
            {
                dasher.Dash();
            }
        }

        //When shooting
        if (Input.GetKey(KeyCode.F))
        {
            projectileShooter1?.TryFire();
        }
    }

    public void UnlockDoubleJump()
    {
        if (jumper != null)
        {
            jumper.doubleJumpAllowed = true;
        }
    }

    public void UnlockWeapon(GameObject projectilePrefab)
    {
        if (projectileShooter1 != null)
        {
            projectileShooter1.projectilePrefab = projectilePrefab;
        }
    }


    public void UnlockDash()
    {
        if (projectileShooter1 != null)
        {
            dasher.dashAllowed = true;
        }
    }

        //probably duplicate teh above function for second projectile???
    
}