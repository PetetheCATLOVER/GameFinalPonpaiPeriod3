using System.Collections;
using UnityEngine;

public class RandomWalker : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float range = 10f; // The radius around the starting point the NPC can wander
    public float minWaitTime = 1f;
    public float maxWaitTime = 4f;

    private Vector3 startPosition;
    private Vector3 targetPosition;

    void Start()
    {
        startPosition = transform.position;
        StartCoroutine(WanderRoutine());
    }

    IEnumerator WanderRoutine()
    {
        while (true)
        {
            // 1. Calculate a random point within the specified range
            Vector3 randomDirection = new Vector3(Random.Range(-range, range), 0, Random.Range(-range, range));
            targetPosition = startPosition + randomDirection;

            // Optional: Constrain the target to a specific plane if you're in 2D or a 3D space with a flat ground
            // For a top-down 2D game, you might want to use new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0)
            //

            // 2. Move towards the target position
            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                yield return null; // Wait until the next frame
            }

            // 3. Wait for a random amount of time before picking a new destination
            float waitTime = Random.Range(minWaitTime, maxWaitTime);
            yield return new WaitForSeconds(waitTime);
        }
    }
}

