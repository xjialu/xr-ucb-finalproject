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

    // Start is called before the first frame update
    void Start()
    {
        if (detectionMeter != null)
        {
            detectionMeter.minValue = 0;
            detectionMeter.maxValue = maxMeterValue;
            detectionMeter.value = currentMeterValue;
        }
    }

    // Update is called once per frame
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

        // Clamp the meter value to ensure it stays between 0 and maxMeterValue
        currentMeterValue = Mathf.Clamp(currentMeterValue, 0, maxMeterValue);

        // Update the slider UI
        if (detectionMeter != null)
        {
            detectionMeter.value = currentMeterValue;
        }

        // Check if the meter is fully filled
        if (currentMeterValue >= maxMeterValue)
        {
            EndGame(); // Call the end game function when the meter is full
        }
    }

    void EndGame()
    {
        Debug.Log("Game Over! You were detected!");
        // Implement your end-game logic here, such as restarting or showing a game over screen
    }
}
