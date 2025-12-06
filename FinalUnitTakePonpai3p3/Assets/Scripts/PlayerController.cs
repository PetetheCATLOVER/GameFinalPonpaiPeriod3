using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float acceleration = 1200f;
    public float steering = 20f;
    public float maxSpeed = 25f;
    public float drag = 2f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -0.5f, 0); // more stability
    }

    void FixedUpdate()
    {
        float move = Input.GetAxis("Vertical");   // W / S
        float turn = Input.GetAxis("Horizontal"); // A / D

        // Limit speed
        if (rb.velocity.magnitude < maxSpeed)
        {
            rb.AddForce(transform.forward * move * acceleration * Time.fixedDeltaTime);
        }

        // Smooth turning
        if (rb.velocity.magnitude > 1f)
        {
            rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, turn * steering * Time.fixedDeltaTime, 0f));
        }

        // Natural drag (smooth slowdown)
        rb.velocity = Vector3.Lerp(rb.velocity, rb.velocity * 0.98f, drag * Time.fixedDeltaTime);
    }
}
