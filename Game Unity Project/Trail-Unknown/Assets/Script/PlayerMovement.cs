using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private Animator anim;

    private bool isFacingRight = true;
    private bool candash = true;
    public bool isDashing;
    [SerializeField] private float dashingPower = 24f;
    [SerializeField] private float dashingTime = 0.2f;
    public float dashingCooldown = 1f;

    [SerializeField] private LayerMask jumpableGround;

    public float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private TrailRenderer tr;

    [SerializeField] private float doubleJumpPower = 7f;

    [SerializeField] private float coyoteTime = 0.5f; // adjust this value as needed
    private float lastTimeOnGround = 0.5f;
    public bool canDoubleJump = false;
    private enum MovementState { idle, running, jumping, falling ,dashing}

    [SerializeField] private AudioSource jumpSoundFX;
    [SerializeField] private AudioSource doublejumpSoundFX;
    [SerializeField] private AudioSource dashSFX;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (isDashing)
        {
            return;
        }

        if (!PauseMenu.GameIsPause)
        {
            ControlMove();

            ControlJump();

            if (Input.GetKeyDown(KeyCode.LeftShift) && candash)
            {
                dashSFX.Play();
                StartCoroutine(Dash());
            }

            Filp();

            UpdateAnimationState();
        }

    }

    private void UpdateAnimationState()
    {
        MovementState state;

        

        if (dirX > 0f)
        {
            state= MovementState.running;
        }
        
        else if (dirX < 0f)
        {
            state = MovementState.running;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }

        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        if (isDashing)
        {
            state = MovementState.dashing;
        }

        anim.SetInteger("state", (int)state);
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }     
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
        if (hit.collider != null)
        {
            lastTimeOnGround = Time.time;
            canDoubleJump = true;
            anim.SetBool("DoubleJump", false);
            return true;
        }
        else
        {
            return false;
        }
    }


    private void Filp()
    {
        if (isFacingRight && dirX < 0f || !isFacingRight && dirX > 0f)
        {
            Vector2 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private IEnumerator Dash()
    {
        candash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting= true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        candash = true;
    }

    private void ControlJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded() || (Time.time - lastTimeOnGround < coyoteTime))
            {
                if (!IsGrounded() && canDoubleJump)
                {
                    canDoubleJump = false;
                    anim.SetBool("DoubleJump", true);
                }
                jumpSoundFX.Play();
                rb.velocity = new Vector2(rb.velocity.x, IsGrounded() ? jumpForce : doubleJumpPower);

                lastTimeOnGround = 0f;
            }
            else if (canDoubleJump)
            {
                doublejumpSoundFX.Play();
                canDoubleJump = false;
                rb.velocity = new Vector2(rb.velocity.x, doubleJumpPower);
                anim.SetBool("DoubleJump", true);
            }
        }
    }

    private void ControlMove()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
    }
 
}
