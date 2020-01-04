using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderController : MonoBehaviour
{
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
