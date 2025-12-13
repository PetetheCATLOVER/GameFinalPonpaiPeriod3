using UnityEngine;

public class NpcSpawner1 : MonoBehaviour
{
    [Header("Npc Settings")]
    public GameObject NpcPrefab;

    [Header("Spawn Settings")]
    public float spawnInterval = 10f;
    public Transform spawnpoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating(nameof(SpawnNpc), 10f, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void SpawnNpc()
    {
        Instantiate(NpcPrefab, spawnpoint.position, spawnpoint.rotation);
        
    
    }
}