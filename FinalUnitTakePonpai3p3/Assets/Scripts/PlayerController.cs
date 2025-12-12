using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public bool hasPowerup = false;
    private float powerupStrength = 15.0f;
    public GameObject powerupIndicator;

    public float moveSpeed = 10f;
    public float turnSpeed = 100f;

    void Update()
    {
        float move = Input.GetAxis("Vertical");   // W/S or Up/Down
        float turn = Input.GetAxis("Horizontal"); // A/D or Left/Right

        // Move forward/backward
        transform.Translate(Vector3.forward * move * moveSpeed * Time.deltaTime);

        // Turn left/right
        transform.Rotate(Vector3.up * turn * turnSpeed * Time.deltaTime);

        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup")) ;
        {
            hasPowerup = true;
            powerupIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);

            Debug.Log("Collided with: " + collision.gameObject.name + " with powerup set to " + hasPowerup);
            enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
        }
    }
}
