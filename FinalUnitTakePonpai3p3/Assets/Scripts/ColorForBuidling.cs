using UnityEngine;

public class ColorForNpc3 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get the Renderer component from the GameObject
        Renderer renderer = GetComponent<Renderer>();

        // Change the color to red
        renderer.material.color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
