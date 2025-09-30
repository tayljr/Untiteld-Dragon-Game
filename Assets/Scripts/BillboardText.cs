using UnityEngine;

public class BillboardText : MonoBehaviour
{
    private Transform camera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Find the main camera in the scene. Ensure your camera has the "MainCamera" tag.
        camera = Camera.main.transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (camera != null)
        {
            // Make the text always face the camera
            transform.LookAt(transform.position + camera.forward);
        }
    }
}
