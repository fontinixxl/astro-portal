using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeMovable : MonoBehaviour
{
    public float speed;
	public float timeToChange;
    Rigidbody2D rigidbody2d;
	float remainingTimeToChange;
    Vector2 direction = Vector2.right;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
		remainingTimeToChange = timeToChange;
    }

    // Update is called once per frame
    void Update()
    {
        remainingTimeToChange -= Time.deltaTime;

		if (remainingTimeToChange <= 0)
		{
			remainingTimeToChange += timeToChange;
			direction *= -1;
		}

		rigidbody2d.MovePosition(rigidbody2d.position + direction * speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            other.collider.GetComponent<PlayerController2D>().ChangeHealth(-1);
        }
    }
}
