using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectionManager : MonoBehaviour
{
    public Slider detectionMeter; // Assign a UI Slider via the Inspector
    public float fillSpeed = 0.2f; // Speed at which the meter fills
    public float decreaseSpeed = 0.1f; // Speed at which the meter decreases
    public float maxMeterValue = 1.0f; // The maximum value of the meter

    private float currentMeterValue = 0.0f; // Tracks the current meter value
    public GameObject player; // Assign the Player GameObject in the Inspector
    public Transform teleportTarget; // Assign the TeleportTarget GameObject in the Inspector
    public ScoreManager scoreManager; // Reference to the ScoreManager

    void Start()
    {
        if (detectionMeter != null)
        {
            detectionMeter.minValue = 0;
            detectionMeter.maxValue = maxMeterValue;
            detectionMeter.value = currentMeterValue;
        }
    }

    void Update()
    {
        bool isDetected = DetectionSystem.isDetected; // Replace this with your detection logic

        if (isDetected)
        {
            currentMeterValue += fillSpeed * Time.deltaTime;
        }
        else
        {
            currentMeterValue -= decreaseSpeed * Time.deltaTime;
        }

        currentMeterValue = Mathf.Clamp(currentMeterValue, 0, maxMeterValue);

        if (detectionMeter != null)
        {
            detectionMeter.value = currentMeterValue;
        }

        if (currentMeterValue >= maxMeterValue)
        {
            EndGame();
        }
    }

    void Teleport()
    {
        if (teleportTarget != null && player != null)
        {
            player.transform.position = teleportTarget.position;
            player.transform.rotation = teleportTarget.rotation; // Optional: Match rotation
        }
        else
        {
            Debug.LogWarning("Teleport target or player is not assigned.");
        }
    }

    void EndGame()
    {
        Debug.Log("Game Over! You were detected!");

        if (scoreManager != null)
        {
            scoreManager.ResetScore();
        }
        else
        {
            Debug.LogWarning("ScoreManager reference is missing!");
        }

        Teleport();
    }
}
