// Platform.cs

using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Platform : MonoBehaviour
{
    private float moveSpeed;
    private float destroyXPosition;
    private string platformTag; // "ShortPlatform" or "MediumPlatform"
    private float platformWidth;

    // We cache the width on Awake
    void Awake()
    {
        // Assumes pivot is centered. Gets the full width.
        platformWidth = GetComponent<BoxCollider2D>().size.x * transform.localScale.x;
    }

    // Call this from the generator to set properties
    public void Setup(float speed, float destroyX, string tag)
    {
        moveSpeed = speed;
        destroyXPosition = destroyX;
        platformTag = tag;
    }

    void Update()
    {
        // Move the platform to the left
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

        // Check if the platform is off-screen to the left
        // We check its right edge
        float rightEdge = transform.position.x + platformWidth / 2;
        if (rightEdge < destroyXPosition)
        {
            // Return to the pool
            gameObject.SetActive(false);
        }
    }

    // Helper function for the generator to know this platform's width
    public float GetWidth()
    {
        if (platformWidth == 0)
        {
            // Fallback in case Awake hasn't run yet
            platformWidth = GetComponent<BoxCollider2D>().size.x * transform.localScale.x;
        }
        return platformWidth;
    }
}