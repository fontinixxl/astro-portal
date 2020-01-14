using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderController : MonoBehaviour
{
    [SerializeField]
    private Collider2D platformCollider;
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerController2D>().LadderZone(false);
            Physics2D.IgnoreCollision(other.GetComponent<Collider2D>(), platformCollider, false);
        }
    }

    void OnTriggerStay2D (Collider2D other)
    {
        if (other.tag == "Player" )
        {
            other.GetComponent<PlayerController2D>().LadderZone(true);
            if (Input.GetAxisRaw("Vertical") != 0f)
            {
                Physics2D.IgnoreCollision(other.GetComponent<Collider2D>(), platformCollider, true);
            }
        }
    }
}
