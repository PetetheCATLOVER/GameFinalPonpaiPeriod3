using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform target;     // Car
    public float distance = 6f;  // How far behind the car
    public float height = 3f;    // How high above the car
    public float followDamping = 5f;
    public float rotationDamping = 5f;

    // How much the camera looks ahead in the car’s forward direction
    public float lookAheadDistance = 10f;

    void LateUpdate()
    {
        if (!target) return;

        // --- Smooth Follow ---
        Vector3 wantedPosition = target.position
                               - target.forward * distance
                               + Vector3.up * height;

        transform.position = Vector3.Lerp(
            transform.position,
            wantedPosition,
            followDamping * Time.deltaTime
        );

        // --- Smooth Rotation (GTA style) ---
        Quaternion wantedRotation = Quaternion.LookRotation(
            target.forward * 2f + target.velocity(), // custom method below
            Vector3.up
        );

        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            wantedRotation,
            rotationDamping * Time.deltaTime
        );

        // --- Look slightly ahead like GTA ---
        Vector3 lookPoint = target.position + target.forward * lookAheadDistance;
        transform.LookAt(lookPoint);
    }
}

// helper extension for velocity (for cars without rigidbody)
public static class TransformExtensions
{
    private static Vector3 lastPos;
    public static Vector3 velocity(this Transform t)
    {
        Vector3 v = (t.position - lastPos) / Time.deltaTime;
        lastPos = t.position;
        return v;
    }
}
