using UnityEngine;

public class NpcSpawner : MonoBehaviour
{
    [Header("NPC Prefabs")]
    public GameObject[] npcPrefabs;     // Assign NPC prefabs here

    [Header("Spawn Settings")]
    public Transform[] spawnPoints;     // Possible spawn positions
    public float spawnInterval = 4f;    // Time between spawns
    public bool randomInterval = false;
    public Vector2 randomIntervalRange = new Vector2(2f, 5f);

    [Header("Spawn Limits")]
    public int maxNPCs = 10;            // Max NPCs alive at once

    private float timer = 0f;
    private int currentNPCs = 0;

    void Update()
    {
        if (currentNPCs >= maxNPCs) return;

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            SpawnNPC();

            if (randomInterval)
                timer = Random.Range(randomIntervalRange.x, randomIntervalRange.y);
            else
                timer = spawnInterval;
        }
    }

    void SpawnNPC()
    {
        if (npcPrefabs.Length == 0 || spawnPoints.Length == 0) return;

        int npcIndex = Random.Range(0, npcPrefabs.Length);
        int spawnIndex = Random.Range(0, spawnPoints.Length);

        GameObject npc = Instantiate(
            npcPrefabs[npcIndex],
            spawnPoints[spawnIndex].position,
            spawnPoints[spawnIndex].rotation
        );

        currentNPCs++;

        // For tracking destruction
        NPCDespawnTracker tracker = npc.AddComponent<NPCDespawnTracker>();
        tracker.spawner = this;
    }

    public void OnNPCDestroyed()
    {
        currentNPCs = Mathf.Max(0, currentNPCs - 1);
    }
}

public class NPCDespawnTracker : MonoBehaviour
{
    public NpcSpawner spawner;

    private void OnDestroy()
    {
        if (spawner != null)
            spawner.OnNPCDestroyed();
    }
}
