using UnityEngine;

public class SwivelPole : MonoBehaviour
{
    public float swivelSpeed = 45f; // Degrees per second
    public float swivelAngle = 45f; // Max swivel angle from center

    private float currentAngle = 0f;
    private bool isSwivelingRight = true;

    void Update()
    {
        float deltaAngle = swivelSpeed * Time.deltaTime;
        if (isSwivelingRight)
        {
            currentAngle += deltaAngle;
            if (currentAngle >= swivelAngle)
            {
                currentAngle = swivelAngle;
                isSwivelingRight = false;
            }
        }
        else
        {
            currentAngle -= deltaAngle;
            if (currentAngle <= -swivelAngle)
            {
                currentAngle = -swivelAngle;
                isSwivelingRight = true;
            }
        }

        transform.localRotation = Quaternion.Euler(0f, currentAngle, 0f);
    }
}
