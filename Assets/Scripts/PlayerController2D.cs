using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    // LayerMask we are using to check if we are grounded.
    [SerializeField] private LayerMask platformsLayerMask;

    public float moveSpeed = 5;
    public float jumpVelocity = 11;
    private Rigidbody2D rigidbody2d;
    private BoxCollider2D boxCollider2d;
    private SpriteRenderer spriteRenderer;
    private Collider2D playerCollider2d;
    private Animator animator;
    [SerializeField]
    private Collider2D platformCollider;
    [SerializeField]
    private float climbingSpeed = 2.5f;
    public bool ladderZone;

    private float gravityScale;

    // private bool ladderZone = false;

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

        float verticalInput = Input.GetAxisRaw("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalVelocity = 0;
        bool climbing = false;

        if (ladderZone && Mathf.Abs(verticalInput) > 0) {
            Physics2D.IgnoreCollision(playerCollider2d, platformCollider, true);
            rigidbody2d.gravityScale = 0;
            verticalVelocity = verticalInput * climbingSpeed;
            climbing = true;
        }

        if (!ladderZone){
            verticalVelocity = rigidbody2d.velocity.y;
            Physics2D.IgnoreCollision(playerCollider2d, platformCollider, false);
            rigidbody2d.gravityScale = gravityScale;
            climbing = false;
        }

        rigidbody2d.velocity = new Vector2(horizontalInput * moveSpeed, verticalVelocity);
        FlipSprite(horizontalInput);

        // -- ANIMATION --
        animator.SetFloat ("velocityX", Mathf.Abs (rigidbody2d.velocity.x) / moveSpeed);
        animator.SetFloat("velocityY", Mathf.Abs(verticalInput) / climbingSpeed);
        animator.SetBool("jumping", !grounded);
        animator.SetBool("climbing", climbing);
    }
    void FlipSprite(float horizontalInput)
    {
        bool flipSprite = (spriteRenderer.flipX ? (horizontalInput > 0.01f) : (horizontalInput < 0f));
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

    public void LadderZone(bool state)
    {
        ladderZone = state;

    }

}
