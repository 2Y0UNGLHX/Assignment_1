using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Vincent Lau
//Assignment 1
//100697262

public class PlayerController : MonoBehaviour
{
    //this one is the moveSpeed.
    public float moveSpeed;
    public float jumpForce;

    private Rigidbody2D rb;
    private Animator charAnim;
    private BoxCollider2D charFeet;
    private bool isGround;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        charAnim = GetComponent<Animator>();
        charFeet = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        Flip();
        Jump();
        Attack();
        CheckGround();
        SwitchAnimation();
    }

    void CheckGround()
    {
        isGround = charFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }

    void Flip()
    {
        bool player_x_Speed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        if(player_x_Speed)
        {
            //why 0.1 instead of 0.0 is because in unity when the velocity is too small,
            //the animation will flip around like crazy, maybe is due to friction

            //our character is facing right so we dont need to flip it if its going right side
            if(rb.velocity.x > 0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }

            //if its going left, then we flip
            if (rb.velocity.x < -0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }

    void Run()
    {
        //This moveDir should be within 0 and 1.
        float moveDir = Input.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(moveDir * moveSpeed, rb.velocity.y);
        rb.velocity = playerVelocity;

        //if there is speed in axis x, set it to true, play animation run
        bool player_x_Speed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        charAnim.SetBool("Run", player_x_Speed);
    }

    void Jump()
    {
        //if(Input.GetButtonDown("Jump"))
        //{
            if(Input.GetButtonDown("Jump"))
            {
                if(isGround)
                {
                    charAnim.SetBool("Jump", true);

                    Vector2 jumpVelocity = new Vector2(0.0f, jumpForce);
                    rb.velocity = Vector2.up * jumpVelocity;
                }
                //charAnim.SetBool("Jump", true);

                //Vector2 jumpVelocity = new Vector2(0.0f, jumpForce);
                //rb.velocity = Vector2.up * jumpVelocity;

            }
        //}
    }

    //void Attack()
    //{
    //    if (Input.GetButtonDown("Attack"))
    //    {
    //        charAnim.SetTrigger("Attack");
    //    }
    //}

    void Attack()
    {
        if (Input.GetButtonDown("Attack"))
        {
            if (!charAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                charAnim.SetTrigger("Attack");
            }
        }
    }


    //I forgot how to blend animations, if I know how, this step will be much easier
    void SwitchAnimation()
    {
        charAnim.SetBool("Idle", false);
        if (charAnim.GetBool("Jump"))
        {
            if (rb.velocity.y < 0.0f)
            {
                charAnim.SetBool("Jump", false);
                charAnim.SetBool("Fall", true);
            }
        }
        else if(isGround)
        {
            charAnim.SetBool("Fall", false);
            charAnim.SetBool("Idle", true);
        }

    }
}
