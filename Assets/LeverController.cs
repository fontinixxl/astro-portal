using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour
{
    public PortalController portalController;
    public Sprite leverUpSprite;
    public Sprite leverDownSprite;
    public bool on { get { return leverOn; } }
    private SpriteRenderer spriteRenderer;
    private bool leverOn;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        leverOn = false;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            spriteRenderer.sprite = leverOn ? leverDownSprite : leverUpSprite;
            leverOn = !leverOn;
            portalController.OpenClosePortal(leverOn);
        }
    }
}
