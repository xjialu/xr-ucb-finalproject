using UnityEngine;
using TMPro;

public class CollisionDetector : MonoBehaviour
{
    public TextMeshProUGUI touchingText; // Assign via inspector
    public static bool isTouching = false; // Global flag for detection

    void Update()
    {
        // Continuously check for collisions between PokeInteractor and Object
        DetectCollisions();
    }

    void DetectCollisions()
    {
        isTouching = false; // Reset detection flag

        // Find all active colliders in the scene
        Collider[] allColliders = FindObjectsOfType<Collider>();

        foreach (Collider colliderA in allColliders)
        {
            // Check if colliderA is a PokeInteractor
            if (colliderA.CompareTag("PokeInteractor"))
            {
                foreach (Collider colliderB in allColliders)
                {
                    // Check if colliderB is an Object
                    if (colliderB.CompareTag("Object"))
                    {
                        // Check if these two are touching
                        if (colliderA.bounds.Intersects(colliderB.bounds))
                        {
                            isTouching = true;
                            UpdateText("Touching!", Color.green);
                            return; // Stop checking further if detected
                        }
                    }
                }
            }
        }

        // If no detection, update the text accordingly
        if (!isTouching)
        {
            isTouching = false;
            UpdateText("Not touching!", Color.red);
        }
    }

    void UpdateText(string message, Color color)
    {
        if (touchingText != null)
        {
            touchingText.text = message;
            touchingText.color = color;
        }
    }
}
