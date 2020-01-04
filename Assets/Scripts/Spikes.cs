using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" )
        {
            other.GetComponent<PlayerController2D>().ChangeHealth(-1);
        }
    }
}
