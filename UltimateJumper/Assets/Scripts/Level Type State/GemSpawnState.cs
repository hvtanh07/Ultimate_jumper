using UnityEngine;

public class GemSpawnState : IState
{
    LevelGenerator owner;
    public void OnEnter(LevelGenerator stateMachine)
    {
        owner = stateMachine;
    }

    public void OnUpdate()
    {
        CalculateAndSpawnNext();
        owner.ChangeState(new SinglePathState());
    }

    public void OnExit()
    {

    }

    public void CalculateAndSpawnNext()
    {
        // Get details from the last *platform*
        Platform lastPlatformScript = owner.lastSpawnedPlatform.GetComponent<Platform>();
        float lastPlatformWidth = lastPlatformScript.GetWidth();
        Vector2 lastPlatformPos = owner.lastSpawnedPlatform.transform.position;
        float lastPlatformRightEdgeX = lastPlatformPos.x + lastPlatformWidth / 2;

        // --- Decide next platform type and get its width ---
        string nextPlatformTag = (Random.value > 0.5f) ? "Short Platform" : "Medium Platform";
        float nextPlatformHalfWidth = (nextPlatformTag == "Short Platform") ? owner.shortPlatformWidth / 2 : owner.mediumPlatformWidth / 2;

        // --- Decide on vertical position ---
        float yChange = Random.Range(owner.minYDistance, owner.maxYDistance);
        float nextY = Mathf.Clamp(lastPlatformPos.y + yChange, owner.minYHeight, owner.maxYHeight);

        // *** GEM SPAWN LOGIC ***

        // 1. Calculate Gem Position
        // X: (Right edge of last platform) + (max distance)
        float gemX = lastPlatformRightEdgeX + owner.maxXDistance;
        // Y: Vertically halfway between the last platform and the next one
        float gemY = (lastPlatformPos.y + nextY) / 2;

        owner.SpawnGemAt(new Vector2(gemX, gemY));

        // 2. Calculate Next Platform Position
        // Its *center* is at: (Gem's X) + (min distance * 1.2) + (half width of new platform)
        float nextPlatformCenterX = gemX + (owner.minXDistance * 1.2f) + nextPlatformHalfWidth;
        Vector2 nextPlatformSpawnPos = new Vector2(nextPlatformCenterX, nextY);

        owner.didSpawnGemLast = true; // Mark that we just spawned a gem

        // Finally, spawn the new platform
        owner.SpawnPlatformAt(nextPlatformSpawnPos, nextPlatformTag, true);
    }
}
