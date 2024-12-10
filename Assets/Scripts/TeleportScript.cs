using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    public Transform teleportTarget; // Assign the TeleportTarget GameObject in the Inspector
    public GameObject player; // Assign the Player GameObject in the Inspector

    public void Teleport()
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
}
