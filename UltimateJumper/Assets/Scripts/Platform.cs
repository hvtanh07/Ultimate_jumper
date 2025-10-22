// Platform.cs

using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Platform : MonoBehaviour
{
    private float moveSpeed;
    private float destroyXPosition;
    private float platformWidth;
    private bool startMoving;

    // We cache the width on Awake
    void Awake()
    {
        // Assumes pivot is centered. Gets the full width.
        platformWidth = GetComponent<BoxCollider2D>().size.x * transform.localScale.x;
    }

    // Call this from the generator to set properties
    public void Setup(float speed, float destroyX, bool gameStart)
    {
        moveSpeed = speed;
        destroyXPosition = destroyX;
        startMoving = gameStart; // If it's game start, don't move yet
    }

    public void StartGame()
    {
        startMoving = true;
    }

    void Update()
    {
        // Game not started don't move just yet
        if (!startMoving) return;
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