using UnityEngine;

public class Gem : MonoBehaviour
{
    private float moveSpeed;
    private float destroyXPosition;
    private const string gemTag = "Gem";

    // Call this from the generator to set properties
    public void Setup(float speed, float destroyX)
    {
        moveSpeed = speed;
        destroyXPosition = destroyX;
    }

    void Update()
    {
        // Move the gem to the left
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

        // Check if the gem is off-screen to the left
        if (transform.position.x < destroyXPosition)
        {
            // Return to the pool
            gameObject.SetActive(false);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        CharacterJump player = collision.gameObject.GetComponent<CharacterJump>();

        if (player == null) return;

        player.ResetAirJumps();
        print("Gem collected!");
        
        //spawn effect here later
        gameObject.SetActive(false);
    }
}
