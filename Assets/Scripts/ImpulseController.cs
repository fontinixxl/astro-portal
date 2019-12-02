using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class ImpulseController : MonoBehaviour
{
    public float thrust = 10.0f;
    public float maxSpeed = 3.0f;
    private Rigidbody2D rb2D;

    private Vector2 startingPosition;

    private Vector2 movement;

    private float sqrMaxVelocity;

    private bool stabilized;

    public float rotationModule;

    public float rotateSpeed = 0.1f;

    Quaternion originalRotation;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        startingPosition = transform.position;
        sqrMaxVelocity = maxSpeed * maxSpeed;
        stabilized = false;
        originalRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        // Reload scene when push Return
        if (Input.GetKeyDown(KeyCode.Return)){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        // if (Input.GetKeyDown(KeyCode.Space) && rb2D.rotation != 0)
        // {
        //     stabilized = true;
        // }

        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        if (!stabilized && rb2D.angularVelocity != 0 && Mathf.Abs(rb2D.angularVelocity) < 10.0f)
        {
            Debug.Log("TIme to bring the character to the original position");
            // float rotation = rb2D.rotation;
            stabilized = true;
        }

        rotationModule = rb2D.rotation % 360.0f;

    }

    void FixedUpdate()
    {

        // Apply Impulse Force registered in Updated
        rb2D.AddForce(movement * thrust * Time.fixedDeltaTime, ForceMode2D.Impulse);

        // Clamp the velocity if it grater than defined maxVelocity
        // https://answers.unity.com/questions/1269757/how-to-get-rigidbody-velocity.html
        Vector2 vel = rb2D.velocity;

        //sqrMagnitude instead of magnitude which is much faster to compute
        if (vel.sqrMagnitude > sqrMaxVelocity) {
            rb2D.velocity = vel.normalized * maxSpeed;
        }

        if (stabilized) {
            transform.rotation = Quaternion.Lerp(transform.rotation,originalRotation,Time.fixedDeltaTime * rotateSpeed);

            if(transform.rotation == originalRotation)
             {
                 stabilized = false;
             }
        }


    }
}
