using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int totalCoins = 5;
    private int collectedCoins = 0;

    public TextMeshProUGUI coinText;
    public TextMeshProUGUI messageText;

    public GameObject restartButton;
    public GameObject startMenu;

    private bool gameOver = false;
    private bool gameStarted = false;

    void Start()
    {
        collectedCoins = 0;
        gameOver = false;
        gameStarted = false;

        UpdateCoinText();

        if (messageText != null)
        {
            messageText.text = "";
        }

        if (restartButton != null)
        {
            restartButton.SetActive(false);
        }

        if (startMenu != null)
        {
            startMenu.SetActive(true);
        }

        Time.timeScale = 0f;
    }

    public void StartGame()
    {
        gameStarted = true;

        if (startMenu != null)
        {
            startMenu.SetActive(false);
        }

        Time.timeScale = 1f;
    }

    public void CollectCoin()
    {
        if (gameOver || !gameStarted)
        {
            return;
        }

        collectedCoins++;
        UpdateCoinText();
    }

    void UpdateCoinText()
    {
        if (coinText != null)
        {
            coinText.text = "Coins: " + collectedCoins + " / " + totalCoins;
        }
    }

    public void TryWin()
    {
        if (gameOver || !gameStarted)
        {
            return;
        }

        if (collectedCoins >= totalCoins)
        {
            gameOver = true;
            Time.timeScale = 0f;

            if (messageText != null)
            {
                messageText.text = "You Win!";
            }

            if (restartButton != null)
            {
                restartButton.SetActive(true);
            }
        }
        else
        {
            if (messageText != null)
            {
                messageText.text = "Collect all coins first!";
            }
        }
    }

    public void GameOver()
    {
        if (gameOver || !gameStarted)
        {
            return;
        }

        gameOver = true;
        Time.timeScale = 0f;

        if (messageText != null)
        {
            messageText.text = "Game Over!";
        }

        if (restartButton != null)
        {
            restartButton.SetActive(true);
        }
    }

    public bool IsGameOver()
    {
        return gameOver || !gameStarted;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}