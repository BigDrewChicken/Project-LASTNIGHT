using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target to Follow")]
    public Transform target; 

    [Header("Camera Settings")]
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    [Header("Level Bounds")]
    public float minX, maxX, minY, maxY;

    void LateUpdate()
    {
        if (target == null) return;


        Vector3 desiredPosition = target.position + offset;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        float clampedX = Mathf.Clamp(smoothedPosition.x, minX, maxX);
        float clampedY = Mathf.Clamp(smoothedPosition.y, minY, maxY);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}
