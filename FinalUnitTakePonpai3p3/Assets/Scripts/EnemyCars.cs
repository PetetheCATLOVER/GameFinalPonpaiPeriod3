using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyCarAI : MonoBehaviour
{
    public Transform player;
    public float moveForce = 1500f;
    public float turnSpeed = 5f;
    public float maxSpeed = 25f;
    public float ramDistance = 6f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -0.5f, 0);
    }

    void FixedUpdate()
    {
        if (!player) return;

        Vector3 dir = (player.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, player.position);

        // Smooth turning toward player
        Quaternion targetRot = Quaternion.LookRotation(dir, Vector3.up);
        rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRot, turnSpeed * Time.fixedDeltaTime));

        // Move forward
        if (rb.linearVelocity.magnitude < maxSpeed)
        {
            rb.AddForce(transform.forward * moveForce * Time.fixedDeltaTime);
        }

        // Extra push when close (ramming behavior)
        if (distance < ramDistance)
        {
            rb.AddForce(transform.forward * (moveForce * 1.5f) * Time.fixedDeltaTime);
        }
    }
}
