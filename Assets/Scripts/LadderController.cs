using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderController : MonoBehaviour
{
    private Rigidbody2D playerRb;
    [SerializeField]
    private Collider2D platformCollider;
    [SerializeField]
    private float climbingSpeed = 2.5f;

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            // Physics2D.IgnoreCollision(other.GetComponent<Collider2D>(), platformCollider, false);
            // Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            // rb.gravityScale = 3.5f;
            // other.GetComponent<Animator>().SetBool("climbing", false);
            // other.GetComponent<Animator>().enabled = true;

            //
            other.GetComponent<PlayerController2D>().ClimbLadder(false);
        }
    }


    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Player" )
        {
            other.GetComponent<PlayerController2D>().ClimbLadder(true);
        }
    }
    void OnTriggerStay2D (Collider2D other)
    {
        float vertical = Input.GetAxis("Vertical");
        if (other.tag == "Player")
        {
            // Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            // if (vertical != 0f)
            // {
            //     other.GetComponent<Animator>().enabled = true;
            //     Physics2D.IgnoreCollision(other.GetComponent<Collider2D>(), platformCollider, true);
            //     rb.gravityScale = 0;
            //     rb.velocity = new Vector2(0, vertical * climbingSpeed);
            //     other.GetComponent<Animator>().SetBool("climbing", true);
            // } else
            // {
            //     // Player is in the ladder but not moving vertically
            //     rb.velocity = Vector2.zero;
            //     other.GetComponent<Animator>().enabled = false;
            // }

        }
    }
}
