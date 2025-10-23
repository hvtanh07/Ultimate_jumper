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
    [HideInInspector] public bool gameStart;

    void Start()
    {
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

    public void ChangeState(IState newState)
    {
        // 1. Perform cleanup on the old state
        currentState?.OnExit();

        // 2. Set the new state
        currentState = newState;

        // 3. Perform setup on the new state
        currentState.OnEnter(this);
    }

    public Platform SpawnPlatformAt(Vector2 spawnPos, string tag, bool gameStart, bool firstPlatform = false)
    {
        GameObject platformObj = ObjectPooling.SharedInstance.SpawnObject(tag, spawnPos, Quaternion.identity, false);

        if (platformObj != null)
        {
            float platformHalfWidth = platformObj.GetComponent<Platform>().GetWidth() / 2;
            if (!firstPlatform)
                platformObj.transform.position = new Vector2(spawnPos.x + platformHalfWidth, spawnPos.y);

            platformObj.GetComponent<Platform>().Setup(moveSpeed, destroyXPosition, gameStart);
            platformObj.SetActive(true);
            //lastSpawnedPlatform = platformObj.GetComponent<Platform>();
            return platformObj.GetComponent<Platform>();
        }
        else
        {
            Debug.LogWarning("Failed to spawn platform with tag: " + tag);
            return null;
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
}