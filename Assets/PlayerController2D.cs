using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    // LayerMask we are using to check if we are grounded.
    [SerializeField] private LayerMask platformsLayerMask;

    public float moveSpeed = 40f;
    public float jumpVelocity = 100f;
    private float horizontalSpeed;
    private Rigidbody2D rigidbody2d;
    private BoxCollider2D boxCollider2d;

    private void Awake()
    {
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody2d.velocity = Vector2.up * jumpVelocity;
        }

        horizontalSpeed = Input.GetAxis("Horizontal") * moveSpeed;
    }

    void FixedUpdate()
    {
        rigidbody2d.velocity = new Vector2(horizontalSpeed, rigidbody2d.velocity.y);

    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, 1f, platformsLayerMask);
        return raycastHit2d.collider != null;
    }

}
