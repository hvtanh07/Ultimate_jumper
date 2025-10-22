
using UnityEngine;
using System.Collections.Generic;
public class NewLevelState : IState
{
    LevelGenerator owner;
    List<Platform> newLevelPlatformList;
    int InitPlatformCount = 3;
    int currentIndex;
    public void OnEnter(LevelGenerator stateMachine)
    {
        owner = stateMachine;

        if (newLevelPlatformList == null)
            newLevelPlatformList = new List<Platform>();
        else
            newLevelPlatformList.Clear();

        // Keep a default but allow it to be changed elsewhere if needed
        InitPlatformCount = 3;
        currentIndex = 0;

        SpawnInitialPlatform();
    }

    public void OnUpdate()
    {
        // Ensure there's always a last spawned platform to base calculations on
        if (owner.lastSpawnedPlatform == null)
        {
            SpawnInitialPlatform();
            return;
        }

        // Spawn the configured number of initial platforms
        for (; currentIndex < InitPlatformCount; currentIndex++)
        {
            CalculateAndSpawnNext();
            newLevelPlatformList.Add(owner.lastSpawnedPlatform);
        }

        // When the game actually starts, activate the platforms and move to the next state
        if (!owner.gameStart) return;

        foreach (Platform platform in newLevelPlatformList)
            platform.StartGame();

        owner.ChangeState(new SinglePathState());
    }

    // Helper to centralize initial platform spawning logic
    private void SpawnInitialPlatform()
    {
        owner.SpawnPlatformAt(owner.initialSpawnPos, "Medium Platform", false);
        if (owner.lastSpawnedPlatform != null)
            newLevelPlatformList.Add(owner.lastSpawnedPlatform);
    }

    public void OnExit()
    {
        newLevelPlatformList.Clear();
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

        // 1. Calculate random horizontal gap
        float xGap = Random.Range(owner.minXDistance, owner.maxXDistance);

        // 2. Calculate Next Platform Position
        // Its *center* is at: (Right edge of last platform) + (gap) + (half width of new platform)
        float nextPlatformCenterX = lastPlatformRightEdgeX + xGap + nextPlatformHalfWidth;
        Vector2 nextPlatformSpawnPos = new Vector2(nextPlatformCenterX, nextY);

        owner.didSpawnGemLast = false; // Mark that we did not spawn a gem

        // Finally, spawn the new platform
        owner.SpawnPlatformAt(nextPlatformSpawnPos, nextPlatformTag, false);
    }
}
