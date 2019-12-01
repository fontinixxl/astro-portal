using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class AsteroidGenController : MonoBehaviour
{
    public GameObject[] asteroidPrefabs;
    public int maxAsteroids = 10;
    private List<Rigidbody2D> asteroidsRb = new List<Rigidbody2D>();
    private float nextAsteroid;
    // Start is called before the first frame update
    void Start()
    {
        nextAsteroid = 4.0f;
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Rigidbody2D rb in asteroidsRb){
            rb.AddForce(new Vector2(0,-100), ForceMode2D.Impulse);
        }

        nextAsteroid -= Time.deltaTime;
        // spawn new asteroid
        if (nextAsteroid <= 0) {
            GameObject asteroid = asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)];
            asteroid.transform.position = new Vector2(-2, 7);
            asteroidsRb.Add(asteroid.GetComponent<Rigidbody2D>());
            Instantiate(asteroid);
            nextAsteroid = 10.5f;
        }
    }
}
