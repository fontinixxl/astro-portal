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
            other.GetComponent<PlayerController2D>().LadderZone(false);
        }
    }


    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Player" )
        {
            other.GetComponent<PlayerController2D>().LadderZone(true);
        }
    }
}
