using UnityEngine;

public class PlayerController : MonoBehaviour
{
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
    }
}
