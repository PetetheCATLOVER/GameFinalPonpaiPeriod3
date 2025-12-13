using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class NpcWanderAndTrigger : MonoBehaviour
{
    [Header("Wandering")]
    public float moveSpeed = 1.5f;
    public float range = 8f;
    public float minWaitTime = 1f;
    public float maxWaitTime = 3f;
    public float raycastHeight = 5f;

    [Header("Trigger")]
    public bool destroyOnHit = true;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private Rigidbody rb;
    private bool triggered = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;
        StartCoroutine(WanderRoutine());
    }

    IEnumerator WanderRoutine()
    {
        while (!triggered)
        {
            // Pick random direction around start point
            Vector3 randomOffset = new Vector3(
                Random.Range(-range, range),
                0,
                Random.Range(-range, range)
            );

            // Raycast to find valid ground
            Vector3 rayStart = startPosition + randomOffset + Vector3.up * raycastHeight;

            RaycastHit hit;
            if (Physics.Raycast(rayStart, Vector3.down, out hit, raycastHeight * 2f))
            {
                targetPosition = hit.point;
            }
            else
            {
                yield return null;
                continue;
            }

            // Move using physics
            while (!triggered && Vector3.Distance(rb.position, targetPosition) > 0.2f)
            {
                Vector3 moveDir = (targetPosition - rb.position).normalized;
                rb.MovePosition(rb.position + moveDir * moveSpeed * Time.fixedDeltaTime);
                yield return new WaitForFixedUpdate();
            }

            // Idle before next wander
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (triggered) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            triggered = true;

            CarWaveSpawner spawner = FindObjectOfType<CarWaveSpawner>();
            if (spawner != null)
            {
                spawner.SpawnWave();
            }

            if (destroyOnHit)
            {
                Destroy(gameObject);
            }
        }
    }
}
