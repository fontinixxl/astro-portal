using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour
{
    public PortalController portalController;
    public Sprite leverUpSprite;
    public Sprite leverDownSprite;
    private SpriteRenderer spriteRenderer;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            spriteRenderer.sprite = leverUpSprite;
            portalController.OpenPortal();
            GetComponent<Collider2D>().enabled = false;
        }
    }
}
