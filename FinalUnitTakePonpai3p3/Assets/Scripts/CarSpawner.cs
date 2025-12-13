using UnityEngine;

public class CarWaveSpawner : MonoBehaviour
{
    public GameObject enemyCarPrefab;
    public Transform player;

    public float spawnRadius = 80f;
    public int carsPerWave = 5;
    public float raycastHeight = 50f;
    public float spawnYOffset = 1.5f;

    public void SpawnWave()
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
                    Vector3 spawnPos = hit.point + Vector3.up * spawnYOffset;

                    Quaternion rotation = Quaternion.LookRotation(
                        player.position - hit.point,
                        Vector3.up
                    );

                    Instantiate(enemyCarPrefab, spawnPos, rotation);
                }
            }
        }
    }
}
