using UnityEngine;
using TMPro;

public class DetectionSystem : MonoBehaviour
{
    public float detectionAngle = 45f; // Half-angle of the detection cone
    public float detectionRange = 20f; // Max detection distance
    public Transform swivelPoint;    // Swiveling pole's origin
    public Transform player;         // Player's position
    public TextMeshProUGUI detectionText; // Assign via inspector

    private bool isDetected = false;

    void OnDrawGizmos()
    {
        if (swivelPoint != null)
        {
            Gizmos.color = isDetected ? Color.red : Color.green;
            Vector3 forward = swivelPoint.forward * detectionRange;
            Gizmos.DrawLine(swivelPoint.position, swivelPoint.position + forward);

            // Draw the cone edges
            Vector3 leftEdge = Quaternion.Euler(0, -detectionAngle, 0) * forward;
            Vector3 rightEdge = Quaternion.Euler(0, detectionAngle, 0) * forward;
            Gizmos.DrawLine(swivelPoint.position, swivelPoint.position + leftEdge);
            Gizmos.DrawLine(swivelPoint.position, swivelPoint.position + rightEdge);
        }
    }


    void Update()
    {
        DetectPlayer();
        if (isDetected)
        {
            Debug.Log("DETECTED");
            // set detection text to detected and change color to red
            detectionText.color = Color.red;
            detectionText.text = "Detected!";
        }
        else
        {
            // set detection text to not detected and change color to green
            detectionText.color = Color.green;
            detectionText.text = "Not Detected";
        }
    }

    void DetectPlayer()
    {
        isDetected = false;

        // Check distance to the player
        Vector3 directionToPlayer = player.position - swivelPoint.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        if (distanceToPlayer <= detectionRange)
        {
            // Check angle within the detection cone
            float angle = Vector3.Angle(swivelPoint.forward, directionToPlayer);
            if (angle <= detectionAngle)
            {
                // Check if the player's poke interactors are touching an object
                Collider[] hits = Physics.OverlapSphere(player.position, 1f); // Adjust radius
                foreach (var hit in hits)
                {
                    if (hit.CompareTag("Object"))
                    {
                        isDetected = true;
                        break;
                    }
                }
            }
        }
    }
}