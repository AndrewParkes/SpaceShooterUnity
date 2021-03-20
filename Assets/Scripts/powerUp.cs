using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUp : MonoBehaviour
{
    private float _speed = 3.5f;
    private float _playBoundY = -6f;

    [SerializeField]
    AudioSource powerUpAudio;

    // Start is called before the first frame update
    void Start()
    {
        powerUpAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            powerUpAudio.Play(0);
        }
    }

    void CalculateMovement()
    {

        Vector3 directon = new Vector3(0, -1, 0) * _speed * Time.deltaTime;

        transform.Translate(directon);

        CalculateDeSpawn();
    }

    void CalculateDeSpawn()
    {
        if (transform.position.y < _playBoundY)
        {
            Destroy(this.gameObject);
        }
    }
}
