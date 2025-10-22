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
    public bool didSpawnGemLast = false;

    // Internal tracking
    private IState currentState;
    [HideInInspector] public Platform lastSpawnedPlatform;
    [HideInInspector] public float shortPlatformWidth;
    [HideInInspector] public float mediumPlatformWidth;
    [HideInInspector] public bool gameStart;

    void Start()
    {
        // Spawn the very first platform to start the chain
        //SpawnPlatformAt(initialSpawnPos, "Medium Platform");
        ChangeState(new NewLevelState()); 
    }

    void Update()
    {
        currentState?.OnUpdate();
        if (Input.GetMouseButtonDown(0) && !gameStart)
        {
            gameStart = true;
        }
    }

    public void SpawnPlatformAt(Vector2 spawnPos, string tag, bool gameStart)
    {
        GameObject platformObj = ObjectPooling.SharedInstance.SpawnObject(tag, spawnPos, Quaternion.identity);
        if (platformObj != null)
        {
            platformObj.GetComponent<Platform>().Setup(moveSpeed, destroyXPosition, gameStart);
            lastSpawnedPlatform = platformObj.GetComponent<Platform>();
        }
    }

    public void SpawnGemAt(Vector2 spawnPos)
    {
        GameObject gemObj = ObjectPooling.SharedInstance.SpawnObject("Gem", spawnPos, Quaternion.identity);
        if (gemObj != null)
        {
            gemObj.GetComponent<Gem>().Setup(moveSpeed, destroyXPosition);
        }
    }

    public void ChangeState(IState newState)
    {
        // 1. Perform cleanup on the old state
        currentState?.OnExit();

        // 2. Set the new state
        currentState = newState;

        // 3. Perform setup on the new state
        currentState.OnEnter(this);
    }

    public float CalculateNextPlatformY(Platform lastPlatform)
    {
        // Get details from the last *platform*
        Platform lastPlatformScript = lastPlatform.GetComponent<Platform>();
        float lastPlatformWidth = lastPlatformScript.GetWidth();
        Vector2 lastPlatformPos = lastPlatform.transform.position;
        float lastPlatformRightEdgeX = lastPlatformPos.x + lastPlatformWidth / 2;

        // --- Decide next platform type and get its width ---
        string nextPlatformTag = (Random.value > 0.5f) ? "Short Platform" : "Medium Platform";
        float nextPlatformHalfWidth = (nextPlatformTag == "Short Platform") ? shortPlatformWidth / 2 : mediumPlatformWidth / 2;

        // --- Decide on vertical position ---
        float yChange = Random.Range(minYDistance, maxYDistance);
        float nextY = Mathf.Clamp(lastPlatformPos.y + yChange, minYHeight, maxYHeight);

        return nextY;
    }
}