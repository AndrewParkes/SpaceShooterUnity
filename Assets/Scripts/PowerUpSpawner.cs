using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _powerUpPrefabs;

    private bool spawn = true;

    private float _playBoundYTop = 8f;
    private float playBoundX = 10;

    private float _nextSpawnRate = 15f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(_nextSpawnRate + Random.Range(0, _nextSpawnRate));
        while (spawn)
        {
            Spawn();
            yield return new WaitForSeconds(_nextSpawnRate + Random.Range(0, _nextSpawnRate));
        }
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnRoutine());
    }

    void Spawn()
    {
        Instantiate(_powerUpPrefabs[Random.Range(0, _powerUpPrefabs.Length)], new Vector3(Random.Range(-1 * playBoundX, playBoundX), _playBoundYTop, transform.position.z), Quaternion.identity);
    }

    public void StopSpawning()
    {
        spawn = false;
    }
}

