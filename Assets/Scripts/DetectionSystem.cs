using UnityEngine;
using TMPro;

public class DetectionSystem : MonoBehaviour
{
    public float detectionAngle = 45f; // Half-angle of the detection cone
    public float detectionRange = 20f; // Max detection distance
    public Transform swivelPoint;    // Swiveling pole's origin
    public Transform player;         // Player's position
    public TextMeshProUGUI detectionText; // Assign via inspector
    public Material lineMaterial;    // Material for the detection cone
    public int coneResolution = 20;  // Number of segments for smoother cone visualization

    private LineRenderer lineRenderer;
    public static bool isDetected = false; // Global flag for detection

    void Start()
    {
        // Initialize the LineRenderer
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = lineMaterial;
        lineRenderer.startWidth = 0.005f;
        lineRenderer.endWidth = 0.005f;
        lineRenderer.loop = false;
        lineRenderer.positionCount = coneResolution + 2; // Cone edges and center
    }

    void Update()
    {
        DetectPlayer();
        DrawDetectionCone();
    }

    void DrawDetectionCone()
    {
        if (swivelPoint == null)
        {
            Debug.LogWarning("Swivel point is not assigned.");
            return;
        }

        // Set line color based on detection status
        lineRenderer.startColor = isDetected ? Color.red : Color.green;
        lineRenderer.endColor = isDetected ? Color.red : Color.green;

        // Calculate cone edges and positions
        Vector3 forward = swivelPoint.forward * detectionRange;
        Vector3 leftEdge = Quaternion.Euler(0, -detectionAngle, 0) * forward;
        Vector3 rightEdge = Quaternion.Euler(0, detectionAngle, 0) * forward;

        // Generate the positions for the cone
        lineRenderer.SetPosition(0, swivelPoint.position); // Start point
        for (int i = 0; i <= coneResolution; i++)
        {
            float angle = Mathf.Lerp(-detectionAngle, detectionAngle, (float)i / coneResolution);
            Vector3 edge = Quaternion.Euler(0, angle, 0) * forward;
            lineRenderer.SetPosition(i + 1, swivelPoint.position + edge);
        }
    }

    void OnDrawGizmos()
    {
        if (swivelPoint != null)
        {
            Gizmos.color = isDetected ? Color.red : Color.green;
            Vector3 forward = swivelPoint.forward * detectionRange;

            // Draw the center line
            Gizmos.DrawLine(swivelPoint.position, swivelPoint.position + forward);

            // Draw the cone edges
            Vector3 leftEdge = Quaternion.Euler(0, -detectionAngle, 0) * forward;
            Vector3 rightEdge = Quaternion.Euler(0, detectionAngle, 0) * forward;
            Gizmos.DrawLine(swivelPoint.position, swivelPoint.position + leftEdge);
            Gizmos.DrawLine(swivelPoint.position, swivelPoint.position + rightEdge);
        }
    }

    void DetectPlayer()
    {
        if (swivelPoint == null || player == null)
        {
            Debug.LogWarning("Swivel point or player is not assigned.");
            return;
        }

        Vector3 directionToPlayer = player.position - swivelPoint.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        if (distanceToPlayer <= detectionRange)
        {

            float angle = Vector3.Angle(swivelPoint.forward, directionToPlayer);
            if (angle <= detectionAngle)
            {
                Debug.Log("Player is in range.");
                // in range and within the detection cone
                if (CollisionDetector.isTouching)
                {
                    Debug.Log("Player is detected!");
                    isDetected = true;
                    detectionText.color = Color.red;
                    detectionText.text = "Detected!";
                }
            }
            else
            {
                Debug.Log("Player is not detected.");
                isDetected = false;
                detectionText.color = Color.green;
                detectionText.text = "Not Detected!";
            }
        }

    }
}
