using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [Header("Npc Prefabs")]
    public GameObject[] NpcPrefabs;          

    [Header("Spawn Settings")]
    public Transform[] spawnPoints;          
    public float spawnInterval = 2f;         
    public bool autoSpawn = true;            

    private float timer;

    void Update()
    {
        if (!autoSpawn) return;

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnNpc();
            timer = 0f;
        }
    }

    public void SpawnNpc()
    {
        if (NpcPrefabs.Length == 0 || spawnPoints.Length == 0)
        {
            Debug.LogWarning("NpcSpawner: Assign NpcPrefabs and spawnPoints!");
            return;
        }

        // Pick a random car
        GameObject NpcToSpawn = NpcPrefabs[Random.Range(0, NpcPrefabs.Length)];
        // Pick a random spawn point
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // Spawn the car
        Instantiate(NpcToSpawn, spawnPoint.position, spawnPoint.rotation);
    }
}

