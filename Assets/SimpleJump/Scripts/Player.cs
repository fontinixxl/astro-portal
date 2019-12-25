/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System;
using UnityEngine;

/*
 * Simple Jump
 * */
public class Player : MonoBehaviour {

    [SerializeField] private LayerMask platformsLayerMask;
    // private Player_Base playerBase;
    private Rigidbody2D rigidbody2d;
    private BoxCollider2D boxCollider2d;

    private void Awake() {
        // playerBase = gameObject.GetComponent<Player_Base>();
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
    }

    private void Update() {
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space)) {
            float jumpVelocity = 100f;
            rigidbody2d.velocity = Vector2.up * jumpVelocity;
        }

        HandleMovement_FullMidAirControl();
        //HandleMovement_SomeMidAirControl();
        //HandleMovement_NoMidAirControl();

        // Set Animations
        if (IsGrounded()) {
            if (rigidbody2d.velocity.x == 0) {
                // playerBase.PlayIdleAnim();
            } else {
                // playerBase.PlayMoveAnim(new Vector2(rigidbody2d.velocity.x, 0f));
            }
        } else {
            // playerBase.PlayJumpAnim(rigidbody2d.velocity);
        }
    }

    private bool IsGrounded() {
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, 1f, platformsLayerMask);
        return raycastHit2d.collider != null;
    }
    
    private void HandleMovement_FullMidAirControl() {
        float moveSpeed = 40f;
        if (Input.GetKey(KeyCode.LeftArrow)) {
            rigidbody2d.velocity = new Vector2(-moveSpeed, rigidbody2d.velocity.y);
        } else {
            if (Input.GetKey(KeyCode.RightArrow)) {
                rigidbody2d.velocity = new Vector2(+moveSpeed, rigidbody2d.velocity.y);
            } else {
                // No keys pressed
                rigidbody2d.velocity = new Vector2(0, rigidbody2d.velocity.y);
            }
        }
    }

    private void HandleMovement_SomeMidAirControl() {
        float moveSpeed = 40f;
        float midAirControl = 3f;
        if (Input.GetKey(KeyCode.LeftArrow)) {
            if (IsGrounded()) {
                rigidbody2d.velocity = new Vector2(-moveSpeed, rigidbody2d.velocity.y);
            } else {
                rigidbody2d.velocity += new Vector2(-moveSpeed * midAirControl * Time.deltaTime, 0);
                rigidbody2d.velocity = new Vector2(Mathf.Clamp(rigidbody2d.velocity.x, -moveSpeed, +moveSpeed), rigidbody2d.velocity.y);
            }
        } else {
            if (Input.GetKey(KeyCode.RightArrow)) {
                if (IsGrounded()) {
                    rigidbody2d.velocity = new Vector2(+moveSpeed, rigidbody2d.velocity.y);
                } else {
                    rigidbody2d.velocity += new Vector2(+moveSpeed * midAirControl * Time.deltaTime, 0);
                    rigidbody2d.velocity = new Vector2(Mathf.Clamp(rigidbody2d.velocity.x, -moveSpeed, +moveSpeed), rigidbody2d.velocity.y);
                }
            } else {
                // No keys pressed
                if (IsGrounded()) {
                    rigidbody2d.velocity = new Vector2(0, rigidbody2d.velocity.y);
                }
            }
        }
    }

    private void HandleMovement_NoMidAirControl() {
        if (IsGrounded()) {
            float moveSpeed = 40f;
            if (Input.GetKey(KeyCode.LeftArrow)) {
                rigidbody2d.velocity = new Vector2(-moveSpeed, rigidbody2d.velocity.y);
            } else {
                if (Input.GetKey(KeyCode.RightArrow)) {
                    rigidbody2d.velocity = new Vector2(+moveSpeed, rigidbody2d.velocity.y);
                } else {
                    // No keys pressed
                    rigidbody2d.velocity = new Vector2(0, rigidbody2d.velocity.y);
                }
            }
        }
    }

}
