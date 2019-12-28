using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    // LayerMask we are using to check if we are grounded.
    [SerializeField] private LayerMask platformsLayerMask;

    public float moveSpeed = 5;
    public float jumpVelocity = 11;
    private float horizontalDirection;
    private float verticalDirection = 0;
    private Rigidbody2D rigidbody2d;
    private BoxCollider2D boxCollider2d;
    private SpriteRenderer spriteRenderer;
    private Collider2D playerCollider2d;
    private Animator animator;
    [SerializeField]
    private Collider2D platformCollider;
    [SerializeField]
    private float climbingSpeed = 2.5f;
    public bool ladder;

    private float gravityScale;

    // private bool ladder = false;

    private void Awake()
    {
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer> ();
        animator = GetComponent<Animator> ();
        playerCollider2d = GetComponent<Collider2D>();

        gravityScale = rigidbody2d.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        bool grounded = IsGrounded();
        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody2d.velocity = Vector2.up * jumpVelocity;
        }
        HandleMovement();

        HandleLadderMovement();

        // -- ANIMATION --
        animator.SetFloat ("velocityX", Mathf.Abs (rigidbody2d.velocity.x) / moveSpeed);
        animator.SetBool("jumping", !grounded);
        // animator.SetFloat("velocityY", rigidbody2d.velocity.y);
    }

    private void HandleMovement()
    {
        // -- HORIZONTAL MOVEMENT --
        // horizontalDirection = Input.GetAxisRaw("Horizontal");
        horizontalDirection = Input.GetAxis("Horizontal");
        rigidbody2d.velocity = new Vector2(horizontalDirection * moveSpeed, rigidbody2d.velocity.y);
        FlipSprite();
    }

    private void HandleLadderMovement()
    {
        float verticalMovement = Input.GetAxisRaw("Vertical");

        if (ladder)
        {
            if (verticalMovement != 0f)
            {
                Physics2D.IgnoreCollision(playerCollider2d, platformCollider, true);
                rigidbody2d.gravityScale = 0;
                rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, verticalMovement * climbingSpeed);
                animator.SetBool("climbing", true);
            }
            else
            {
                rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, 0);
            }
        }
        else
        {
            Physics2D.IgnoreCollision(playerCollider2d, platformCollider, false);
            rigidbody2d.gravityScale = gravityScale;
            animator.SetBool("climbing", false);
        }
        animator.SetFloat("velocityY", Mathf.Abs(verticalMovement));
    }
    void FlipSprite()
    {
        bool flipSprite = (spriteRenderer.flipX ? (horizontalDirection > 0.01f) : (horizontalDirection < 0f));
        if (flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider2d.bounds.center,
            boxCollider2d.bounds.size, 0f, Vector2.down, 1f, platformsLayerMask);

        return raycastHit2d.collider != null;
    }

    public void ClimbLadder(bool climbing)
    {
        ladder = climbing;

    }

}
