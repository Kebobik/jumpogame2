using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    PlayerControls controls;
    float direction = 0;
    public Rigidbody2D playerRB;
    public float speed = 400;
    public Animator animator;
    bool isFacingRight = true;
    public float jumpforce = 5;
    int numberOfJumps = 0;
    bool isGrounded;
    public Transform groundCheck;
    public LayerMask groundLayer;
    private void Awake()
    {
            controls = new PlayerControls();
            controls.Enable();
            controls.Land.Move.performed += ctx =>
            {
                direction = ctx.ReadValue<float>();
            };
       controls.Land.Jump.performed += ctx => Jump();
    }
    

    
    void FixedUpdate()
    {
        
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f,groundLayer);
        animator.SetBool("isGrounded", isGrounded);
        Debug.Log(isGrounded);
        playerRB.velocity = new Vector2(direction * speed * Time.fixedDeltaTime, playerRB.velocity.y);
        animator.SetFloat("Speed", Mathf.Abs(direction));
        
        if(isFacingRight && direction <0 || !isFacingRight && direction >0)
        Flip(); 
        
    }
    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }
     void Jump()
    {
        if (isGrounded)
        {
            numberOfJumps = 0;
            playerRB.velocity = new Vector2(playerRB.velocity.x, jumpforce);
            numberOfJumps++;
            AudioManager.instance.Play("FirstJump");
        }
        else 
        { 
            if(numberOfJumps == 1 )
                playerRB.velocity = new Vector2(playerRB.velocity.x, jumpforce);
                numberOfJumps++;
                  AudioManager.instance.Play("SecondJump");
        }
       
    }
}
