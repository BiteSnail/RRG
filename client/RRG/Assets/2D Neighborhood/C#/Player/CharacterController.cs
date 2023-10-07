using System.Collections;
using UnityEngine;

namespace CharacterController
{
    public class CharacterController : MonoBehaviour
{
    public ParticleSystem dust;

    private Animator anim;

    private float horizontal;
    private bool isJumping;
    private float coyoteTimeCounter;
    private float jumpBufferCounter;
    
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    public float baseSpeed;
    [SerializeField]private float speed = 8f;
    [SerializeField]private float jumpingPower = 16f;
    [SerializeField]private float coyoteTime = 0.2f;
    [SerializeField]private float jumpBufferTime = 0.5f;

    private void Start() 
    {
        anim = GetComponent<Animator>();
    }
    
    private void Update()
    {
        //Player Jump

        horizontal = Input.GetAxisRaw("Horizontal");

        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            CreateDust();

            isJumping = true;

            anim.SetTrigger("takeOff");

            isJumping = true;

            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;

            isJumping = false;
        }

        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f && !isJumping )
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);

            jumpBufferCounter = 0f;

            StartCoroutine(JumpCooldown());
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);

            isJumping = false;

            coyoteTimeCounter = 0f;
        }

        if (rb.velocity.y == 0)
        {
            anim.SetBool("isJumping", false);
        }else
        {
            anim.SetBool("isJumping", true);
        }

        if (rb.velocity.y > 0)
        {
            anim.SetBool("isJumping", true);
        }else
        {
            anim.SetBool("isJumping", false);
        }
    }

    private void FixedUpdate()
    {
        //Player Movement

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        if (horizontal == 0)
        {
            anim.SetBool("isWalking", false);
        }
        else
        {
            anim.SetBool("isWalking", true);
        }

        if (Input.GetKey (KeyCode.LeftShift))
        {
            speed = baseSpeed * 2f;

            if ((Mathf.Abs(horizontal) > 0) && rb.velocity.y == 0 && Input.GetKey (KeyCode.LeftShift))
            {
                anim.SetBool("isRunning", true);
            }
            else
            {
                anim.SetBool("isRunning", false);
            }
        }else
        {
            speed = baseSpeed;

            if ((Mathf.Abs(horizontal) > 0) && rb.velocity.y == 0 && Input.GetKey (KeyCode.LeftShift))
            {
                anim.SetBool("isRunning", true);
            }
            else
            {
                anim.SetBool("isRunning", false);
            }
        }

        if(horizontal > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }else if (horizontal < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;

            CreateDust();
        }
    }

    private IEnumerator JumpCooldown()
    {
        isJumping = true;

        yield return new WaitForSeconds(0.1f);
        
        isJumping = false;
    }
    
    void CreateDust()
    {
        dust.Play();
    }

}

}
