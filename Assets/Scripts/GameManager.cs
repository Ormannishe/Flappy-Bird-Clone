// 2025-08-25 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using UnityEngine;
using TMPro;
using System.Collections;
using System.Runtime.CompilerServices;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverCanvas; // Reference to the Game Over Canvas
    public GameObject titleScreenCanvas; // Reference to the Title Screen Canvas
    public TMP_Text scoreText; // Reference to the UI Text element for the score
    public TMP_Text highScoreText; // Reference to the UI Text element for the high score

    public AudioClip gameOverSound;

    private int score = 0; // The player's score
    private bool isGameOver = false;
    private static bool isFirstLaunch = true;

    private AudioSource audioSource;

    void Start()
    {
        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();

        if (isFirstLaunch)
        {
            Time.timeScale = 0f; // Pause the game
            titleScreenCanvas.SetActive(true); // Show the Title Screen
        }
    }

    public void StartGame()
    {
        Time.timeScale = 1f; // Resume the game
        titleScreenCanvas.SetActive(false); // Hide the Title Screen
        isFirstLaunch = false;
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }

    public bool IsTitleScreenUp()
    {
        return isFirstLaunch;
    }

    public void GameOver()
    {
        if (!isGameOver)
        {
            isGameOver = true;

            // Update the high score
            int highScore = PlayerPrefs.GetInt("HighScore");
            if (score > highScore)
            {
                PlayerPrefs.SetInt("HighScore", score);
                highScoreText.text = "New High Score!";
            }
            else
            {
                highScoreText.text = "High Score: " + highScore;
            }

            gameOverCanvas.SetActive(true); // Show the Game Over screen
            Time.timeScale = 0f; // Pause the game
            audioSource.Stop(); // Stop the background music

            // Start a coroutine to play the game over sound with a delay
            StartCoroutine(PlayGameOverSoundWithDelay(0.5f)); // Adjust the delay (1f = 1 second)
        }
    }

    private IEnumerator PlayGameOverSoundWithDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay); // Wait for the specified delay in real time
        if (gameOverSound != null)
        {
            audioSource.PlayOneShot(gameOverSound);
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Resume the game
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name); // Reload the current scene
    }

    public void AddScore(int amount)
    {
        score += amount; // Increase the score
        scoreText.text = "" + score; // Update the UI
    }
}