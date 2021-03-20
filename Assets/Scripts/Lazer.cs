using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{

    private float _speed = 15f;

    private float _playBoundY = 14;

    private int damageDelt = 20;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        CalculateDeSpawn();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(this.gameObject);
            Enemy enemy = other.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.Damage(damageDelt);
            }
        }
        if (other.tag == "Asteroid")
        {
            Destroy(this.gameObject);
        }
    }

    void CalculateMovement()
    {

        Vector3 directon = new Vector3(0, 1, 0) * _speed * Time.deltaTime;

        transform.Translate(directon);
    }
    
        
    void CalculateDeSpawn()
    {
        if(transform.position.y > _playBoundY) 
        {
            Destroy(this.gameObject);
        }
    }
}
