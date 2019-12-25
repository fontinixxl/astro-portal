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
    private Rigidbody2D rigidbody2d;
    private BoxCollider2D boxCollider2d;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private void Awake()
    {
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer> ();
        animator = GetComponent<Animator> ();
        // DEBUG
        rigidbody2d.gravityScale = 3.5f;
    }

    // Update is called once per frame
    void Update()
    {
        bool grounded = IsGrounded();
        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody2d.velocity = Vector2.up * jumpVelocity;
        }

        horizontalDirection = Input.GetAxis("Horizontal");

        bool flipSprite = (spriteRenderer.flipX ? (horizontalDirection > 0.01f) : (horizontalDirection < 0f));
        if (flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        animator.SetFloat ("velocityX", Mathf.Abs (rigidbody2d.velocity.x) / moveSpeed);
        animator.SetBool("jumping", !grounded);
    }

    void FixedUpdate()
    {
        rigidbody2d.velocity = new Vector2(horizontalDirection * moveSpeed, rigidbody2d.velocity.y);

    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider2d.bounds.center,
            boxCollider2d.bounds.size, 0f, Vector2.down, 1f, platformsLayerMask);

        return raycastHit2d.collider != null;
    }

}
