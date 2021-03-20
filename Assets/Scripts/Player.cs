using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float _speed = 13f;

    private float playBoundX = 11;

    [SerializeField]
    private GameObject _laserPreFab;

    [SerializeField]
    private float _fireRate = 0.25f;

    [SerializeField]
    private GameObject _fireRight;

    [SerializeField]
    private GameObject _leftFire;

    [SerializeField]
    private GameObject _thruster;

    private float _nextFireRate = 0.25f;

    private int health = 3;

    private bool trippleShot = false;
    private bool shield = false;
    private float speed2 = 1f;
    private float speedBoost = 1.2f;

    [SerializeField]
    private GameObject _shieldVisualizer;

    [SerializeField]
    AudioSource deathAudio;

    [SerializeField]
    AudioSource powerUpAudio;

    UIManager ui;

    Animator amimator;

    [SerializeField]
    private int player = 1;

    // Start is called before the first frame update
    void Start()
    {
        //starting pos set = (0,0,0)
        if(player == 1)
        {
            transform.position = new Vector3(0, -4, 0);
        }
        else if (player == 2)
        {
            transform.position = new Vector3(3, -4, 0);
        }

        ui = GameObject.Find("UIManager").GetComponent<UIManager>();
        deathAudio = GetComponent<AudioSource>();
        amimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        Fire();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PowerUpTrippleShot")
        {
            Destroy(other.gameObject, 0.1f);
            EnableTrippleShot();
        }
        if (other.tag == "PowerUpSpeed")
        {
            Destroy(other.gameObject, 0.1f);
            EnableSpeed2();
        }
        if (other.tag == "PowerUpShield")
        {
            Destroy(other.gameObject, 0.1f);
            EnableShield();
        }
    }

    void Fire()
    {
        if(((player == 1 && Input.GetKey(KeyCode.Space)) || (player == 2 && Input.GetKey(KeyCode.Keypad0))) && Time.time > _nextFireRate)
        {
            _nextFireRate = Time.time + _fireRate;
            Instantiate(_laserPreFab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);

            if(trippleShot)
            {
                Instantiate(_laserPreFab, transform.position + new Vector3(-0.5f, 0f, 0), Quaternion.Euler(0, 0, 15));
                Instantiate(_laserPreFab, transform.position + new Vector3(0.5f, 0f, 0), Quaternion.Euler(0, 0, -15));
            }
        }
    }

    void CalculateMovement()
    {
        float inputH = 0;
        float inputV = 0;
        if (player ==1 )
        {
            inputH = Input.GetAxis("HorizontalP1");
            inputV = Input.GetAxis("VerticalP1");
        }
        else if (player == 2)
        {
            inputH = Input.GetAxis("HorizontalP2");
            inputV = Input.GetAxis("VerticalP2");
        }

        Vector3 directon = new Vector3(inputH, inputV, 0) * _speed * speed2 * Time.deltaTime;

        transform.Translate(directon);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4, 6), transform.position.z);

        if (transform.position.x > playBoundX)
        {
            transform.position = new Vector3(-1 * playBoundX, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -1 * playBoundX)
        {
            transform.position = new Vector3(1 * playBoundX, transform.position.y, transform.position.z);
        }
    }

    public void Damage(int amount)
    {
        if (shield)
        {
            shield = false;
            _shieldVisualizer.SetActive(false);
        }
        else
        {
            UpdateHealth(-1);
            trippleShot = false;
            speed2 = 1f;
            if (health <= 0)
            {
                deathAudio.Play(0);
                amimator.SetTrigger("onDeath");
                Destroy(this.gameObject, 1.5f);
                _leftFire.SetActive(false);
                _fireRight.SetActive(false);
                _thruster.SetActive(false);

            }

            if (health == 2)
            {
                _fireRight.SetActive(true);
            } 
            else if (health == 1)
            {
                _leftFire.SetActive(true);
            }
        }
    }

    public void EnableTrippleShot()
    {
        trippleShot = true;
    }

    public void EnableShield()
    {
        shield = true;
        _shieldVisualizer.SetActive(true);
    }

    public void EnableSpeed2()
    {
        speed2 = speedBoost;
    }

    public void AddScore(int amount)
    {
        ui.AddToScore(amount);
    }

    public void UpdateHealth(int amount)
    {
        health += amount;
        ui.SetLives(health, player);
    }
}
