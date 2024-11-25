using UnityEngine;
using TMPro; // Import for TextMeshPro support

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Assign via inspector
    private int score = 0;

    // Method to update the score
    public void AddScore(int value)
    {
        Debug.Log("Score added: " + value);
        score += value;
        scoreText.text = "Score: " + score;
    }
}
