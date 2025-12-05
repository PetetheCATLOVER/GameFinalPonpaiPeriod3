using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Private Variables
    private float speed = 10.0f;
    private float turnSpeed = 25.0f;
    private float horizontal1Input;
    private float fowardInput;
    public Camera mainCamera;
    public Camera hoodCamera;
    public KeyCode switchKey;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         // this is where we get player input
        horizontal1Input = Input.GetAxis("Horizontal");
        fowardInput = Input.GetAxis("Vertical");

        // We'll move this vecihle foward
        transform.Translate(Vector3.forward * Time.deltaTime * speed * fowardInput);
        // We turn the vehicle
        transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontal1Input);

        if(Input.GetKeyDown(switchKey))
        {
            mainCamera.enabled = !mainCamera.enabled;
            hoodCamera.enabled = !hoodCamera.enabled;
        } 
    }
    }

