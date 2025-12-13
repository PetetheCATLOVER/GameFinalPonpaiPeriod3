using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class NpcWanderAndTrigger : MonoBehaviour
{
    [Header("Wander Settings")]
    public float moveSpeed = 2f;
    public float range = 10f;
    public float minWaitTime = 1f;
    public float maxWaitTime = 3f;
    public float raycastHeight = 5f;
    public LayerMask groundLayer;      // layers NPC can walk on
    public LayerMask obstacleLayer;    // layers NPC cannot pass through

    [Header("Trigger Settings")]
    private bool triggered = false;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;
        StartCoroutine(WanderRoutine());
    }

    IEnumerator WanderRoutine()
    {
        while (true)
        {
            // Pick a random point within range
            Vector3 randomOffset = new Vector3(
                Random.Range(-range, range),
                0,
                Random.Range(-range, range)
            );
            Vector3 potentialTarget = startPosition + randomOffset + Vector3.up * raycastHeight;

            // Raycast down to find ground
            RaycastHit hit;
            if (Physics.Raycast(potentialTarget, Vector3.down, out hit, raycastHeight * 2f, groundLayer))
            {
                targetPosition = hit.point;
            }
            else
            {
                targetPosition = rb.position; // fallback: stay in place
            }

            // Move towards target safely
            while (Vector3.Distance(rb.position, targetPosition) > 0.1f)
            {
                Vector3 direction = (targetPosition - rb.position).normalized;

                // Check for obstacles ahead
                if (Physics.Raycast(rb.position + Vector3.up * 0.5f, direction, 0.5f, obstacleLayer))
                {
                    break; // stop moving if hitting a wall/building
                }

                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
                yield return new WaitForFixedUpdate();
            }

            // Wait randomly before next move
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (triggered) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            triggered = true;

            // Trigger car wave
            CarWaveSpawner spawner = FindObjectOfType<CarWaveSpawner>();
            if (spawner != null)
            {
                spawner.SpawnWave();
            }

            // Optional: destroy NPC after hit
            Destroy(gameObject);
        }
    }
}
