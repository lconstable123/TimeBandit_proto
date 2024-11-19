using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Facecamera : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        // Get the main camera reference
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Make the sprite face the camera (billboarding)
        Vector3 direction = mainCamera.transform.position - transform.position;
        direction.y = 0; // Lock the rotation so it only rotates around the Y-axis (for 3D)
        
        // Rotate the object towards the camera
        transform.rotation = Quaternion.LookRotation(direction);
    }
}
