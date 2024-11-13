using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerCollision : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public GameObject finishText;
    public float timer = 60f;
    private int score = 0;


    public AudioSource audioSource; 
    public AudioClip CorrectPickUpSound;
    public AudioClip WrongPickUpSound;
    public AudioClip GameOverSound;


    void Start()
    {
        // Initialize score display
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
        else
        {
            Debug.LogError("ScoreText is not assigned.");
        }

        // Initialize timer display
        if (timerText != null)
        {
            timerText.text = "Time: " + Mathf.Round(timer);
        }
        else
        {
            Debug.LogError("TimerText is not assigned.");
        }
    }

    void Update()
    {
        // Update timer and check for end of game
        timer -= Time.deltaTime;
        if (timerText != null)
        {
            timerText.text = "Time: " + Mathf.Round(timer);
        }

        if (timer <= 0)
        {
            EndGame();
        }
    }

    public void SetTimer(float newTime)
    {
        timer = newTime;
        UpdateTimerText(); // Updates the display if needed
    }

    void UpdateTimerText()
    {
        if (timerText != null)
        {
            timerText.text = "Time: " + Mathf.Round(timer);
        }
    }

    public void AddScore(int points)
    {
        // Update score
        score += points;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player collides with a Correct or Wrong pickup
        if (other.CompareTag("CorrectPickUp"))
        {
            AddScore(10);
            audioSource.PlayOneShot(CorrectPickUpSound);
            other.gameObject.SetActive(false);
        }
        else if (other.CompareTag("WrongPickUp"))
        {
            AddScore(-5);
            audioSource.PlayOneShot(WrongPickUpSound);
            other.gameObject.SetActive(false);
        }
    }
    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    void EndGame()
    {
        audioSource.PlayOneShot(GameOverSound);
        finishText.gameObject.SetActive(true);
        Debug.Log("Game Over");
    }
}
