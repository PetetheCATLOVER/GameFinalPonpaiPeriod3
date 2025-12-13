using UnityEngine;

public class NpcHitTrigger : MonoBehaviour
{
    private bool triggered = false;

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

            Destroy(gameObject);
        }
    }
}
