using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public bool hasPowerup;

    public Transform target;   // Drag your car here in Inspector
    public Vector3 offset = new Vector3(0, 5, -7);
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        if (!target) return;

        // Follow position
        Vector3 desiredPosition = target.position + target.forward * offset.z + Vector3.up * offset.y;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Look in front of the car
        transform.LookAt(target.position + target.forward * 10f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"));
        {
            hasPowerup = true;
            Destroy(other.gameObject); 
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Debug.Log("Collided with: " +  collision.gameObject.name + " with powerup set to " + hasPowerup);
        }
    }
}
