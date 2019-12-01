using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class ImpulseController : MonoBehaviour
{
    public float thrust = 10.0f;
    public float maxSpeed = 3.0f;
    public float torqueForce = 5.0f;
    private Rigidbody2D rb2D;

    private Vector2 startingPosition;

    private Vector2 movement;

    private float sqrMaxVelocity;

    private int torqueDirection;

    public bool stabilize;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        startingPosition = transform.position;
        sqrMaxVelocity = maxSpeed * maxSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // Reload scene when push Return
        if (Input.GetKeyDown(KeyCode.Return)){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            // transform.position = startingPosition;
            // rb2D.velocity = new Vector3(0f,0f,0f);
            // // reset rotation and rotation velocity
            // transform.rotation = Quaternion.identity;
            // rb2D.angularVelocity = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Space) && rb2D.rotation != 0)
        {
            stabilize = true;
        }

        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        bool torqueLeft = Input.GetMouseButton(0);
        bool torqueRight = Input.GetMouseButton(1);

        if (torqueLeft || torqueRight)
        {
            torqueDirection = torqueLeft ? 1 : -1;
        }

    }

    void FixedUpdate()
    {
        // Apply Impulse Force registered in Updated
        rb2D.AddForce(movement * thrust * Time.fixedDeltaTime, ForceMode2D.Impulse);

        // Apply torque force registred in Updated
        // torqueDirection is gonna determine the direction (1 or -1)
        rb2D.AddTorque(torqueDirection * torqueForce * Time.fixedDeltaTime);

        // Clamp the velocity if it grater than defined maxVelocity
        // https://answers.unity.com/questions/1269757/how-to-get-rigidbody-velocity.html
        Vector2 vel = rb2D.velocity;

        //sqrMagnitude instead of magnitude which is much faster to compute
        if (vel.sqrMagnitude > sqrMaxVelocity) {
            rb2D.velocity = vel.normalized * maxSpeed;
        }
    }
}
