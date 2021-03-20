using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField]
    private int score = 0;

    [SerializeField]
    private int highScore = 0;

    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private Text highScoreText;

    [SerializeField]
    private Text gameOver;

    [SerializeField]
    private Text restartText;

    [SerializeField]
    private Image[] livesImages;

    [SerializeField]
    private Sprite[] liveSprites;

    private bool flicker = false;

    private bool isOver = false;

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore"); 
        scoreText.text = "Score: " + score;
        highScoreText.text = "Best: " + highScore;
        foreach(Image livesImage in livesImages)
        {
            livesImage.sprite = liveSprites[liveSprites.Length - 1];
        }
        
        gameOver.gameObject.SetActive(false);
        restartText.gameObject.SetActive(false);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator FlickerText()
    {
        while (true)
        {
            flicker = (flicker == false);
            gameOver.gameObject.SetActive(flicker);
            restartText.gameObject.SetActive(!flicker);
            yield return new WaitForSeconds(1);
        }
    }

    public void SetLives(int amount, int player)
    {
        if (player - 1 >= 0 && amount >= 0)
        {
            livesImages[player - 1].sprite = liveSprites[amount];
            if (amount == 0)
            {
                gameManager.GameOver();
            }
        }
    }

    public void GameOverScreen()
    {
        StartCoroutine(FlickerText());
    }

    public void AddToScore(int amount)
    {
        score += amount;
        scoreText.text = "Score: " + score;
        if(score > highScore)
        {
            highScore = score;
            highScoreText.text = "Best: " + highScore;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
    }

    public void updateHighScore()
    {

    }
}
