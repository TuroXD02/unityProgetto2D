using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* AGGIUNGERE ANIMAZIONE SALTO 0 velocity da animator */

public class Player : MonoBehaviour
{

    Rigidbody2D rb;
    Animator animator;
    [SerializeField]Collider2D standingCollider;
    [SerializeField]Transform groundCheckCollider;
    [SerializeField]Transform overheadCheckCollider;
    [SerializeField]LayerMask groundLayer;

    const float groudCheckRadius = 0.07f;
    const float overheadCheckRadius = 0.07f;
    float horizontalValue;
    float crouchSpeedModifier = 0.5f;
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] int jumps;
    int availableJumps;

    bool multipleJumps = false;
    bool coyoteJump;
    bool isGrounded = false;
    bool crouch = false;
    bool facingRight = true;

    void Awake()
    {
        availableJumps = jumps;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!CanMove())
            return; 

        horizontalValue = Input.GetAxisRaw("Horizontal");

        //animator
        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));

        //buttons
        if (Input.GetButtonDown("Jump"))
            Jump();
        if (Input.GetButtonDown("Crouch"))
            crouch = true;
        else if (Input.GetButtonUp("Crouch"))
            crouch = false;

        //velocità per animazione salto
        animator.SetFloat("yVelocity", rb.velocity.y);
    }

    void FixedUpdate()
    {
        GroundCheck();
        Move(horizontalValue, crouch);
    }

    void Move(float dir, bool crouchFlag)
    {
        #region crouch

        if(!crouchFlag)
        {
            if(Physics2D.OverlapCircle(overheadCheckCollider.position, overheadCheckRadius, groundLayer))
                crouchFlag = true;  
        }
        
        animator.SetBool("Crouch", crouchFlag);
        standingCollider.enabled = !crouchFlag;
        #endregion        

        #region movimento
        //movimento
        float xVal = dir * speed * Time.fixedDeltaTime * 10;                 //deltaTime adatta la speed ai frame 
        if (crouchFlag)
            xVal *= crouchSpeedModifier;
        Vector2 targetVelocity = new Vector2(xVal, rb.velocity.y);
        rb.velocity = targetVelocity;

        //flip dello sprite
        if (facingRight && dir<0)
        {
            transform.localScale = new Vector3 (-4, 4, 4);
            facingRight= false;
        }
        else if (!facingRight && dir>0)
        {
            transform.localScale = new Vector3 (4, 4, 4);
            facingRight = true;
        }
        #endregion 
                        
    }    

    void GroundCheck()
    {
        bool wasGrounded = isGrounded;
        isGrounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, groudCheckRadius, groundLayer);
        if (colliders.Length > 0)
        {
            isGrounded = true;
            if (!wasGrounded)
            {
                availableJumps = jumps;
                multipleJumps= false;
            }
        }
        else
        {
            if(wasGrounded)
                StartCoroutine(CoyoteJumpDelay());

        }

        //se groundati animator.bool jump va disabilitato
        animator.SetBool("Jump", !isGrounded);
    }

    IEnumerator CoyoteJumpDelay()
    {
        coyoteJump = true;
        yield return new WaitForSeconds(.2f);
        coyoteJump = false;
    }

    void Jump()
    {
        if (isGrounded)
        {
            multipleJumps= true;
            availableJumps--;
            rb.velocity = Vector2.up * jumpForce;
            animator.SetBool("Jump", true);
        }
        else
        {
            if (coyoteJump)
            {
                multipleJumps = true;
                availableJumps--;
                rb.velocity = Vector2.up * jumpForce;
                animator.SetBool("Jump", true);
            }

            if (multipleJumps && availableJumps > 0)
            {
                availableJumps--;
                rb.velocity = Vector2.up * jumpForce;
                animator.SetBool("Jump", true);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(groundCheckCollider.position, groudCheckRadius);
        Gizmos.DrawSphere(overheadCheckCollider.position, overheadCheckRadius);
    }

    bool CanMove()
    {
        bool can = true;
        if(FindObjectOfType<InteractionSystem>().isExamining)
            can = false;
        if (FindObjectOfType<InventorySystem>().isOpen)
            can= false;
        return can; 
    }

}
