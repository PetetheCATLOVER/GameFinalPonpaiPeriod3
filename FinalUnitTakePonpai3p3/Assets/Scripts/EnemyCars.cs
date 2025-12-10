using UnityEngine;

public class EnemyCarChase : MonoBehaviour
{
    public float speed = 10f;
    public float turnSpeed = 5f;
    public float followDistance = 3f;

    private Rigidbody rb;
    private Transform target;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Find player automatically
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            target = playerObj.transform;
        }
    }

    void FixedUpdate()
    {
        if (target == null) return;

        float distance = Vector3.Distance(transform.position, target.position);
        if (distance < followDistance) return;

        Vector3 direction = (target.position - transform.position).normalized;

        Quaternion lookRotation = Quaternion.LookRotation(direction);
        rb.MoveRotation(Quaternion.Slerp(transform.rotation, lookRotation, turnSpeed * Time.fixedDeltaTime));

        rb.MovePosition(rb.position + transform.forward * speed * Time.fixedDeltaTime);
    }
}
