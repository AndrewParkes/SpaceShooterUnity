using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private float _speed = 5f;
    private float _playBoundYTop = 8f;
    private float _playBoundYBottom = -6f;
    private float playBoundX = 10;

    private float health = 50f;

    private int damageDelt = 1;
    private int score = 10;

    UIManager ui;

    Animator amimator;

    [SerializeField]
    AudioSource deathAudio;

    [SerializeField]
    private GameObject _enemyFire;

    private float _minFireRate = 3f;
    private float _maxFireRate = 7f;

    // Start is called before the first frame update
    void Start()
    {
        ui = GameObject.Find("UIManager").GetComponent<UIManager>();
        amimator = GetComponent<Animator>();
        deathAudio = GetComponent<AudioSource>();
        StartCoroutine(FireRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null && damageDelt != 0)
            {
                player.Damage(damageDelt);
                Destroy();
            }
        }
    }

    IEnumerator FireRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(_minFireRate, _maxFireRate));
            SpawnEnemyLazer();
        }
    }

    void SpawnEnemyLazer()
    {
        if (damageDelt != 0)
        {
            GameObject enemy = Instantiate(_enemyFire, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        }
    }

    void CalculateMovement()
    {

        Vector3 directon = new Vector3(0, -1, 0) * _speed * Time.deltaTime;

        transform.Translate(directon);

        if (transform.position.y < _playBoundYBottom && damageDelt != 0)
        {
            transform.position = new Vector3(Random.Range(-1 * playBoundX, playBoundX), _playBoundYTop, transform.position.z);
        }
    }

    public void Damage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Destroy();
        }
    }

    private void Destroy()
    {
        if (damageDelt != 0)
        {
            damageDelt = 0;
            ui.AddToScore(10);
            amimator.SetTrigger("onEnemyDeath");
            deathAudio.Play(0);
            Destroy(this.gameObject, 2.5f);
        }
    }
}
