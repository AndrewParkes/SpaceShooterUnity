using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public bool isGameOver = false;

    [SerializeField]
    public int numberOfPlayersAlive = 1;
    public int numberOfPlayers = 1;

    UIManager uiManager;

    private EnemySpawner enemySpawner;
    private PowerUpSpawner powerUpSpawner;

    [SerializeField]
    private Canvas pauseMenue;


    // Start is called before the first frame update
    void Start()
    {
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        powerUpSpawner = GameObject.Find("PowerUpSpawner").GetComponent<PowerUpSpawner>();
        numberOfPlayers = numberOfPlayersAlive;
    }

    // Update is called once per frame
    void Update()
    {
        checkForGameRestart();
    }

    private void checkForGameRestart()
    {
        if (Input.GetKeyDown(KeyCode.R) && isGameOver)
        {
            if(numberOfPlayers == 1)
            {
                SceneManager.LoadScene(1);
            }
            else if (numberOfPlayers == 2)
            {
                SceneManager.LoadScene(2);
            }

        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MainMenu();
        }
        
        if (Input.GetKeyDown(KeyCode.P))
        {
            Pause();
        }
    }

    public void GameOver()
    {
        numberOfPlayersAlive--;
        if(numberOfPlayersAlive == 0)
        {
            isGameOver = true;
            enemySpawner.StopSpawning();
            powerUpSpawner.StopSpawning();
            uiManager.GameOverScreen();
        }
    }

    public void Pause()
    {
        pauseMenue.gameObject.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void Resume()
    {
        pauseMenue.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void MainMenu()
    {
        Resume();
        SceneManager.LoadScene(0);
    }
}
