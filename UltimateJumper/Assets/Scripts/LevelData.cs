using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData", order = 1)]
public class LevelData : ScriptableObject
{
    [Header("Movement")]
    public float moveSpeed = 5f;

    [Header("Spawning Controls")]
    public Vector2 initialSpawnPos = new Vector2(2.74f, 0);
    public float spawnTriggerX = 15f; // When right edge of last platform hits this X, spawn next
    public float destroyXPosition = -15f; // When platforms pass this X, despawn

    [Header("Platform Spacing")]
    public float minXDistance = 4f;
    public float maxXDistance = 7f;
    public float minYDistance = -4f;
    public float maxYDistance = 3f;

    [Header("World Height Limits")]
    public float minYHeight = -6f;
    public float maxYHeight = 3f;

    [Header("Gem Spawning")]
    [Range(0, 1)]
    public float gemSpawnChance = 0.2f;


    [Space(10)]
    [Header("Character Jump Settings")]

    [Header("Jump Settings")]
    public float initialJumpVelocity = 15f;
    public float maxJumpTime = 0.35f;
    public float baseGravityScale = 4f;
    public float fallMultiplier = 1.5f;
    public float jumpHoldGravityMultiplier = 0.5f;

    [Header("Double Jump Settings")]
    public int airJumpsValue = 1;
    public float airJumpDelay = 0.5f;


    [Header("Ground Check")]
    public Vector2 groundCheckBoxSize = new Vector2(0.5f, 0.1f);

}
