using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private TextMeshProUGUI GameOverHighscore_1;
    [SerializeField] private TextMeshProUGUI GameOverHighScore_2;
    [SerializeField] private TextMeshProUGUI GameOverscore_1;
    [SerializeField] private TextMeshProUGUI GameOverscore_2;

    [SerializeField] private TextMeshProUGUI StartHighscore_1;
    [SerializeField] private TextMeshProUGUI StartHighScore_2;

    [SerializeField] private TextMeshProUGUI InGameScore_1;
    [SerializeField] private TextMeshProUGUI InGameScore_2;

    [SerializeField] private GameObject gameoverScreen;
    [SerializeField] private GameObject startScreen;
    [SerializeField] private GameObject InGameScreen;
    [SerializeField] private GameObject obstacleManager;
    [SerializeField] private Rigidbody2D playerRB;

    private int currentScore = 0;
    private bool gameStarted = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (!PlayerPrefs.HasKey("Highscore"))
        {
            PlayerPrefs.SetInt("Highscore", 0);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        startScreen.SetActive(true);
        StartHighscore_1.text = PlayerPrefs.GetInt("Highscore").ToString();
        StartHighScore_2.text = PlayerPrefs.GetInt("Highscore").ToString();
    }

    public void GameOverScreen()
    {
        gameoverScreen.SetActive(true);
        InGameScreen.SetActive(false);

        if (currentScore > PlayerPrefs.GetInt("Highscore"))
        {
            PlayerPrefs.SetInt("Highscore", currentScore);
        }

        GameOverHighscore_1.text = PlayerPrefs.GetInt("Highscore").ToString();
        GameOverHighScore_2.text = PlayerPrefs.GetInt("Highscore").ToString();
        GameOverscore_1.text = currentScore.ToString();
        GameOverscore_2.text = currentScore.ToString();
    }

    public bool IsGameStarted()
    {
        return gameStarted;
    }

    public void IncrementScore()
    {
        currentScore++;
        InGameScore_1.text = currentScore.ToString();
        InGameScore_2.text = currentScore.ToString();
    }

    public void StartGame()
    {
        startScreen.SetActive(false);
        InGameScreen.SetActive(true);
        gameStarted = true;
        currentScore = 0;
        InGameScore_1.text = currentScore.ToString();
        InGameScore_2.text = currentScore.ToString();
        playerRB.bodyType = RigidbodyType2D.Dynamic;
    }

    public void RestartGame()
    {
        gameoverScreen.SetActive(false);
        currentScore = 0;
        InGameScreen.SetActive(true);
        InGameScore_1.text = currentScore.ToString();
        InGameScore_2.text = currentScore.ToString();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
