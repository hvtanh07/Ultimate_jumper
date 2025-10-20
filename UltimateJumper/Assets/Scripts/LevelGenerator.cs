// LevelGenerator.cs

using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    [Header("Movement")]
    public float moveSpeed = 5f;

    [Header("Spawning Controls")]
    public Vector2 initialSpawnPos = Vector2.zero;
    public float spawnTriggerX = 15f; // When right edge of last platform hits this X, spawn next
    public float destroyXPosition = -15f; // When platforms pass this X, despawn

    [Header("Platform Spacing")]
    public float minXDistance = 2f;
    public float maxXDistance = 5f;
    public float minYDistance = -1.5f;
    public float maxYDistance = 2.5f;

    [Header("World Height Limits")]
    public float minYHeight = -4f;
    public float maxYHeight = 6f;

    [Header("Gem Spawning")]
    [Range(0, 1)]
    public float gemSpawnChance = 0.5f;
    private bool didSpawnGemLast = false;

    // Internal tracking
    private Transform lastSpawnedPlatform;
    private float shortPlatformWidth;
    private float mediumPlatformWidth;

    void Start()
    {
        // Spawn the very first platform to start the chain
        SpawnPlatformAt(initialSpawnPos, "Medium Platform");
    }

    void Update()
    {
        if (lastSpawnedPlatform == null)
        {
            // Failsafe if the first platform was somehow returned to pool
            SpawnPlatformAt(initialSpawnPos, "Medium Platform");
            return;
        }

        // Calculate the position of the right edge of the last spawned platform
        float lastPlatformWidth = lastSpawnedPlatform.GetComponent<Platform>().GetWidth();
        float lastPlatformRightEdge = lastSpawnedPlatform.position.x + lastPlatformWidth / 2;

        // If the right edge has moved far enough to the left, spawn the next element
        if (lastPlatformRightEdge < spawnTriggerX)
        {
            CalculateAndSpawnNext();
        }
    }

    void CalculateAndSpawnNext()
    {
        // Get details from the last *platform*
        Platform lastPlatformScript = lastSpawnedPlatform.GetComponent<Platform>();
        float lastPlatformWidth = lastPlatformScript.GetWidth();
        Vector2 lastPlatformPos = lastSpawnedPlatform.position;
        float lastPlatformRightEdgeX = lastPlatformPos.x + lastPlatformWidth / 2;

        // --- Decide next platform type and get its width ---
        string nextPlatformTag = (Random.value > 0.5f) ? "Short Platform" : "Medium Platform";
        float nextPlatformHalfWidth = (nextPlatformTag == "Short Platform") ? shortPlatformWidth / 2 : mediumPlatformWidth / 2;

        // --- Decide on vertical position ---
        float yChange = Random.Range(minYDistance, maxYDistance);
        float nextY = Mathf.Clamp(lastPlatformPos.y + yChange, minYHeight, maxYHeight);

        // --- Decide if we spawn a gem ---
        bool spawnGem = (Random.value < gemSpawnChance) && !didSpawnGemLast;

        Vector2 nextPlatformSpawnPos;

        if (spawnGem)
        {
            // *** GEM SPAWN LOGIC (Based on your specific rule) ***

            // 1. Calculate Gem Position
            // X: (Right edge of last platform) + (max distance)
            float gemX = lastPlatformRightEdgeX + maxXDistance;
            // Y: Vertically halfway between the last platform and the next one
            float gemY = (lastPlatformPos.y + nextY) / 2;

            SpawnGemAt(new Vector2(gemX, gemY));

            // 2. Calculate Next Platform Position
            // Its *center* is at: (Gem's X) + (min distance) + (half width of new platform)
            float nextPlatformCenterX = gemX + (minXDistance * 1.2f) + nextPlatformHalfWidth;
            nextPlatformSpawnPos = new Vector2(nextPlatformCenterX, nextY);

            didSpawnGemLast = true; // Mark that we just spawned a gem
        }
        else
        {
            // *** NO GEM SPAWN LOGIC ***

            // 1. Calculate random horizontal gap
            float xGap = Random.Range(minXDistance, maxXDistance);

            // 2. Calculate Next Platform Position
            // Its *center* is at: (Right edge of last platform) + (gap) + (half width of new platform)
            float nextPlatformCenterX = lastPlatformRightEdgeX + xGap + nextPlatformHalfWidth;
            nextPlatformSpawnPos = new Vector2(nextPlatformCenterX, nextY);

            didSpawnGemLast = false; // Mark that we did not spawn a gem
        }

        // Finally, spawn the new platform
        SpawnPlatformAt(nextPlatformSpawnPos, nextPlatformTag);
    }

    void SpawnPlatformAt(Vector2 spawnPos, string tag)
    {
        GameObject platformObj = ObjectPooling.SharedInstance.SpawnObject(tag, spawnPos, Quaternion.identity);
        if (platformObj != null)
        {
            platformObj.GetComponent<Platform>().Setup(moveSpeed, destroyXPosition, tag);
            lastSpawnedPlatform = platformObj.transform;
        }
    }

    void SpawnGemAt(Vector2 spawnPos)
    {
        GameObject gemObj = ObjectPooling.SharedInstance.SpawnObject("Gem", spawnPos, Quaternion.identity);
        if (gemObj != null)
        {
            gemObj.GetComponent<Gem>().Setup(moveSpeed, destroyXPosition);
        }
    }
}