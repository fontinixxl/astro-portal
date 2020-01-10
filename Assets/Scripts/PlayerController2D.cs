using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    // LayerMask we are using to check if we are grounded.
    [SerializeField] private LayerMask platformsLayerMask;

    public float moveSpeed = 5;
    public float jumpVelocity = 11;
    private Rigidbody2D rigidbody2d;
    private BoxCollider2D boxCollider2d;
    private SpriteRenderer spriteRenderer;
    private Collider2D playerCollider2d;
    private Animator animator;
    [SerializeField]
    private float climbingSpeed = 2.5f;
    private bool ladderZone;
    private bool climbing;
    private float gravityScale;
    private bool playerCanMove;
    public int maxHealth = 1;
    public int health { get { return currentHealth; } }
    int currentHealth;
    AudioSource audioSource;
    public AudioClip jumpSound;
    public AudioClip playerHit;
    public Transform respawnPosition;
    public bool playerGrounded;

    private void Awake()
    {
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer> ();
        animator = GetComponent<Animator> ();
        playerCollider2d = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
        gravityScale = rigidbody2d.gravityScale;
        climbing = false;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.playerCanMove)
            return;

        bool grounded = IsGrounded();
        //DEGUG:
        playerGrounded = grounded;
        //END DEBUG
        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody2d.velocity = Vector2.up * jumpVelocity;
            audioSource.PlayOneShot(jumpSound);
        }

        float verticalInput = Input.GetAxisRaw("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        // initial Y velocity to zero in case Player is not moving while climbing
        float verticalVelocity = 0;

        // player is climbing (applying vertical movement) in the ladder
        if (ladderZone && Mathf.Abs(verticalInput) > 0.01f) {
            // Physics2D.IgnoreCollision(playerCollider2d, platformCollider, true);
            rigidbody2d.gravityScale = 0;
            verticalVelocity = verticalInput * climbingSpeed;
            climbing = true;
        }
        // player not climbing
        if (!ladderZone){
            verticalVelocity = rigidbody2d.velocity.y;
            // if (playerCollider2d != null)
                // Physics2D.IgnoreCollision(playerCollider2d, platformCollider, false);
            rigidbody2d.gravityScale = gravityScale;
            climbing = false;
        }

        FlipSprite(horizontalInput);
        rigidbody2d.velocity = new Vector2(horizontalInput * moveSpeed, verticalVelocity);

        // -- ANIMATION --;
        animator.SetFloat ("velocityX", Mathf.Abs (rigidbody2d.velocity.x) / moveSpeed);
        animator.SetFloat("velocityY", Mathf.Abs(verticalInput) / climbingSpeed);
        animator.SetBool("jumping", !grounded);
        animator.SetBool("climbing", climbing);
    }
    void FlipSprite(float horizontalInput)
    {
        bool flipSprite = (spriteRenderer.flipX ? (horizontalInput > 0.01f) : (horizontalInput < 0f));
        if (flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider2d.bounds.center,
            boxCollider2d.bounds.size, 0f, Vector2.down, 0.2f, platformsLayerMask);

        return raycastHit2d.collider != null;
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            audioSource.PlayOneShot(playerHit);
            // if (isInvincible)
            //     return;

            // animator.SetTrigger("Hit");

            // Instantiate(hitEffect, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
            // PlaySound(HitClip);

            // isInvincible = true;
            // invincibleTimer = timeInvincible;
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        if (currentHealth == 0) {
            DisablePlayerMovements();
            Invoke("Respawn", .5f);
        }
        // UIHealthBar.Instance.SetValue(currentHealth / (float)maxHealth);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Exit" && other.GetComponent<PortalController>().isOpen)
        {
            DisablePlayerMovements();
            Invoke("NextLevel", 1f);
        }
    }


    public void DisablePlayerMovements()
    {
        GameManager.instance.playerCanMove = false;
        rigidbody2d.velocity = Vector2.zero;
        rigidbody2d.gravityScale = 0;
        animator.enabled = false;
    }
    private void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    void Respawn()
    {
        ChangeHealth(maxHealth);
        transform.position = respawnPosition.position;
        GameManager.instance.playerCanMove = true;
        animator.enabled = true;
        rigidbody2d.gravityScale = gravityScale;
    }

    public void LadderZone(bool state)
    {
        ladderZone = state;
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
