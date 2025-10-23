using UnityEngine;

public class DividedLevelState : IState
{
    LevelGenerator owner;
    int dividedLength;
    Platform lastUpper, lastLower;
    public float minXDistance = 2f;
    public float maxXDistance = 5f;
    public float minYDistance = -1.5f;
    public float maxYDistance = 2.5f;


    public float UpperZone = 3f;
    public float LowerZone = 3f;

    public void OnEnter(LevelGenerator stateMachine)
    {
        owner = stateMachine;
        //SplitStateStart();
    }

    public void OnUpdate()
    {
        // Calculate the position of the right edge of the last spawned platform
        float lastPlatformWidth = owner.lastSpawnedPlatform.GetWidth();
        float lastPlatformRightEdge = owner.lastSpawnedPlatform.transform.position.x + lastPlatformWidth / 2;

        // If the right edge has moved far enough to the left, spawn the next element
        if (lastPlatformRightEdge < owner.spawnTriggerX)
        {
            CalculateAndSpawnNext();
        }
    }

    public void OnExit()
    {

    }

    public void CalculateAndSpawnNext()
    {
        // --- Decide next platform type and get its width ---
        string nextLowerPlatformTag = (Random.value > 0.5f) ? "Short Platform" : "Medium Platform";
        string nextUpperPlatformTag = (Random.value > 0.5f) ? "Short Platform" : "Medium Platform";

        Vector2 nextLowerplatformPos = CalculateNextPlatformPosition(lastLower, owner.minYHeight, owner.minYHeight + LowerZone);
        //Vector2 nextUpperplatformPos = CalculateNextPlatformPosition(lastUpper, owner.maxYHeight - UpperZone, owner.maxYHeight);

        owner.lastSpawnedPlatform = lastLower = owner.SpawnPlatformAt(nextLowerplatformPos, nextLowerPlatformTag, true);
        //lastUpper = owner.SpawnPlatformAt(nextUpperplatformPos, nextUpperPlatformTag, true);
    }

    public void SplitStateStart()
    {
        // --- Decide next platform type and get its width ---
        string nextLowerPlatformTag = (Random.value > 0.5f) ? "Short Platform" : "Medium Platform";

        Vector2 nextLowerplatformPos = CalculateNextPlatformPosition(lastLower, owner.minYHeight, owner.minYHeight + LowerZone);
        //Vector2 nextUpperplatformPos = CalculateNextPlatformPosition(lastUpper, owner.maxYHeight - UpperZone, owner.maxYHeight);

        owner.lastSpawnedPlatform = lastLower = owner.SpawnPlatformAt(nextLowerplatformPos, nextLowerPlatformTag, true);  
    }

    public Vector2 CalculateNextPlatformPosition(Platform lastPlatform, float minHeight, float maxHeight)
    {
        // Get details from the last *platform*
        Platform lastPlatformScript = lastPlatform.GetComponent<Platform>();
        float lastPlatformWidth = lastPlatformScript.GetWidth();
        Vector2 lastPlatformPos = lastPlatform.transform.position;
        float lastPlatformRightEdgeX = lastPlatformPos.x + lastPlatformWidth / 2;

        // --- Decide on vertical position ---
        float yChange = Random.Range(minYDistance, maxYDistance);
        float nextY = Mathf.Clamp(lastPlatformPos.y + yChange, minHeight, maxHeight);

        // 1. Calculate random horizontal gap
        float xGap = Random.Range(minXDistance, maxXDistance);

        // 2. Calculate Next Platform Position
        // Its *center* is at: (Right edge of last platform) + (gap) + (half width of new platform)
        float nextPlatformCenterX = lastPlatformRightEdgeX + xGap;
        Vector2 nextPlatformSpawnPos = new Vector2(nextPlatformCenterX, nextY);

        owner.didSpawnGemLast = false; // Mark that we did not spawn a gem

        return nextPlatformSpawnPos;
    }
}
