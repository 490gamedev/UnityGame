using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class scaleBG : MonoBehaviour
{
    void Start()
    {
        FitToCamera();
    }

    void FitToCamera()
    {
        // Get the sprite renderer component
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("Background object does not have a SpriteRenderer component.");
            return;
        }

        // Get the camera's orthographic size
        UnityEngine.Camera camera = UnityEngine.Camera.main;
        if (camera == null || !camera.orthographic)
        {
            Debug.LogError("No main camera found or the camera is not orthographic.");
            return;
        }

        // Calculate the background size
        float screenHeight = camera.orthographicSize * 2f;
        float screenWidth = screenHeight * camera.aspect;

        // Set the scale of the background to fit the camera
        transform.localScale = new Vector3(screenWidth / spriteRenderer.sprite.bounds.size.x, screenHeight / spriteRenderer.sprite.bounds.size.y, 1);

        // Position the background at the camera's position
        transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y, 0);
    }
}

/*public class scaleBG : MonoBehaviour
{
    //were going to calculate the width and the height of the gameObject
    // Start is called before the first frame update

    void Start()
    {
        FitToCamera();
    }

    void FitToCamera()
    {
        // Get the sprite renderer component
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("Background object does not have a SpriteRenderer component.");
            return;
        }

        // Get the camera's orthographic size
        UnityEngine.Camera camera = UnityEngine.Camera.main;
        if (camera == null || !camera.orthographic)
        {
            Debug.LogError("No main camera found or the camera is not orthographic.");
            return;
        }

        // Calculate the background size
        float screenHeight = camera.orthographicSize * 2f;
        float screenWidth = screenHeight * camera.aspect;

        // Set the scale of the background to fit the camera
        transform.localScale = new Vector3(screenWidth / spriteRenderer.sprite.bounds.size.x, screenHeight / spriteRenderer.sprite.bounds.size.y, 1);

        // Position the background at the camera's position
        transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y, 0);
    }





}*/
