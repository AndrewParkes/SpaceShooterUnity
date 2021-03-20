using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField]
    private GameObject _enemyPreFab;
    [SerializeField]
    private GameObject _enemyContainer;

    private bool spawn = true;

    private float _playBoundYTop = 8f;
    private float playBoundX = 10;

    private float _nextSpawnRate = 3f;

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
        yield return new WaitForSeconds(_nextSpawnRate);
        while (spawn)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(_nextSpawnRate);
        }
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnRoutine());
    }

    void SpawnEnemy()
    {
        GameObject enemy = Instantiate(_enemyPreFab, new Vector3(Random.Range(-1 * playBoundX, playBoundX), _playBoundYTop, transform.position.z), Quaternion.identity);
        enemy.transform.SetParent(_enemyContainer.transform);
    }

    public void StopSpawning()
    {
        spawn = false;
    }
}
