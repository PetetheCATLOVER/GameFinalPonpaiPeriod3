using UnityEngine;

public class NpcSpawner : MonoBehaviour
{
    public GameObject npcPrefab;
    public int spawnAmount = 10;

    public Vector2 spawnAreaSize = new Vector2(100, 100);
    public float raycastHeight = 50f;
    public float spawnYOffset = 1.2f; // adjust if needed

    void Start()
    {
        SpawnNPCs();
    }

    void SpawnNPCs()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            Vector3 randomPos = new Vector3(
                Random.Range(-spawnAreaSize.x / 2f, spawnAreaSize.x / 2f),
                raycastHeight,
                Random.Range(-spawnAreaSize.y / 2f, spawnAreaSize.y / 2f)
            );

            RaycastHit hit;

            if (Physics.Raycast(randomPos, Vector3.down, out hit, 200f))
            {
                // Do NOT spawn on roads
                if (hit.collider.gameObject.layer != LayerMask.NameToLayer("Road"))
                {
                    Vector3 spawnPos = hit.point + Vector3.up * 5f;
                    Instantiate(npcPrefab, spawnPos, Quaternion.identity);

                }
            }
        }
    }
}
