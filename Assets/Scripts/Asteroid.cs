using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    [SerializeField]
    private GameObject _explosionPreFab;

    private float rotationSpeed = 20.0f;

    private EnemySpawner enemySpawner;
    private PowerUpSpawner powerUpSpawner;

    private bool gameBegun = false;

    [SerializeField]
    AudioSource deathAudio;

    // Start is called before the first frame update
    void Start()
    {
        enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        powerUpSpawner = GameObject.Find("PowerUpSpawner").GetComponent<PowerUpSpawner>();
        deathAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }

    void Rotate()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Lazer" && !gameBegun)
        {
            gameBegun = true;
            GameObject explosion = Instantiate(_explosionPreFab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);

            deathAudio.Play(0);

            enemySpawner.StartSpawning();
            powerUpSpawner.StartSpawning();
            Destroy(this.gameObject, 1.0f);


        }
    }
}
