using UnityEngine;

public class NpcSpawner : MonoBehaviour
{
    public GameObject npcPrefab;
    public int spawnAmount = 10;
    public Vector2 spawnAreaSize = new Vector2(100, 100);
    public LayerMask roadLayer;  // assign Road layer in Inspector

    void Start()
    {
        SpawnNPCs();
    }

    void SpawnNPCs()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            Vector3 spawnPos = GetValidSpawnPosition();
            Instantiate(npcPrefab, spawnPos + Vector3.up * 3f, Quaternion.identity);
        }
    }

    Vector3 GetValidSpawnPosition()
    {
        Vector3 pos;
        int safety = 0; // avoid infinite loops

        do
        {
            float x = Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2);
            float z = Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2);

            pos = new Vector3(x, 0, z);
            safety++;

            // Prevent endless checking if something goes wrong
            if (safety > 50) break;

        } while (IsOnRoad(pos));

        return pos;
    }

    bool IsOnRoad(Vector3 position)
    {
        // raycast down to see if it hits the road layer
        return Physics.Raycast(position + Vector3.up * 10f, Vector3.down, 20f, roadLayer);
    }
}
