using UnityEngine;

public class CarWaveSpawner : MonoBehaviour
{
    public GameObject enemyCarPrefab;
    public Transform player;

    public float spawnRadius = 80f;
    public int carsPerWave = 5;
    public float timeBetweenWaves = 10f;
    public float raycastHeight = 50f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeBetweenWaves)
        {
            SpawnWave();
            timer = 0f;
        }
    }

    void SpawnWave()
    {
        for (int i = 0; i < carsPerWave; i++)
        {
            Vector3 randomPos = player.position + Random.insideUnitSphere * spawnRadius;
            randomPos.y = player.position.y + raycastHeight;

            RaycastHit hit;

            if (Physics.Raycast(randomPos, Vector3.down, out hit, 200f))
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Road"))
                {
                    Instantiate(enemyCarPrefab, hit.point, hit.transform.rotation);
                }
            }
        }
    }
}
