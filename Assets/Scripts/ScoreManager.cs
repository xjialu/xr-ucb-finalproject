using UnityEngine;
using TMPro; // Import for TextMeshPro support

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Assign via inspector
    public TextMeshProUGUI lastScoreText; // Assign via inspector
    private int score = 0;

    // Method to update the score
    public void AddScore(int value)
    {
        Debug.Log("Score added: " + value);
        score += value;
        scoreText.text = "Score: " + score;
        lastScoreText.text = "Last Score: " + score;
    }

    // Method to reset the score
    public void ResetScore()
    {
        Debug.Log("Score reset");
        score = 0;
        scoreText.text = "Score: " + score;
    }

}
