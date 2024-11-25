using UnityEngine;

public class FallDetector : MonoBehaviour
{
    public ScoreManager scoreManager;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object has a Rigidbody (only items can score)
        if (other.tag == "object")
        {
            // log to console about detection
            Debug.Log("Item fell off!");

            // Add score when the item falls off
            scoreManager.AddScore(1);

            // Optionally destroy the item after it falls
            // Destroy(other.gameObject, 2f);
        }
        else
        {
            Debug.Log("Non-item object fell off!");
            // Debug.Log("Object name: " + other.name);
            Debug.Log(other);
        }
    }
}
