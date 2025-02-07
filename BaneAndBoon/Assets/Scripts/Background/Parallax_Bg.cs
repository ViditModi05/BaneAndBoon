using UnityEngine;

public class Parallax_Bg : MonoBehaviour
{
    [Header("References")]
    private Camera cam;

    [Header("Settings")]
    [SerializeField] private float parallaxEffect;
    private float startX;
    private float length;

    void Start()
    {
        cam = Camera.main;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        startX = transform.position.x;
    }

    void Update()
    {
        // Calculate how much the background should move relative to the camera's movement
        float distanceToMove = cam.transform.position.x * parallaxEffect;

        // Update the background's position based on the camera's movement and the parallax effect
        transform.position = new Vector3(startX + distanceToMove, transform.position.y, transform.position.z);

        // Get the current camera position
        float camPositionX = cam.transform.position.x;

        // When the camera moves past the background's length, reposition the background to avoid gaps
        if (camPositionX > startX + length)
        {
            startX += length;  // Move the start position forward to create a seamless loop
        }
        else if (camPositionX < startX - length)
        {
            startX -= length;  // Move the start position backward to create a seamless loop
        }
    }
}
