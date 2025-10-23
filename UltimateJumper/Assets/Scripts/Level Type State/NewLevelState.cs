
using UnityEngine;
using System.Collections.Generic;
public class NewLevelState : IState
{
    LevelGenerator owner;
    List<Platform> newLevelPlatformList;
    int InitPlatformCount = 3;
    int currentIndex;
    float minXDistance = 2f;
    float maxXDistance = 5f;
    float minYDistance = -1.5f;
    float maxYDistance = 2.5f;


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
        minXDistance = owner.minXDistance;
        maxXDistance = owner.maxXDistance;
        minYDistance = owner.minYDistance;
        maxYDistance = owner.maxYDistance;

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

        owner.ChangeState(new DividedLevelState());
    }

    // Helper to centralize initial platform spawning logic
    private void SpawnInitialPlatform()
    {
        owner.lastSpawnedPlatform = owner.SpawnPlatformAt(owner.initialSpawnPos, "Medium Platform", false, true);
        if (owner.lastSpawnedPlatform != null)
            newLevelPlatformList.Add(owner.lastSpawnedPlatform);
    }

    public void OnExit()
    {
        newLevelPlatformList.Clear();
    }

    public void CalculateAndSpawnNext()
    {
        // --- Decide next platform type and get its width ---
        string nextPlatformTag = (Random.value > 0.5f) ? "Short Platform" : "Medium Platform";

        Vector2 nextplatformPos = CalculateNextPlatformPosition(owner.lastSpawnedPlatform, false);

        // Finally, spawn the new platform
        owner.lastSpawnedPlatform = owner.SpawnPlatformAt(nextplatformPos, nextPlatformTag, false);
    }

    public Vector2 CalculateNextPlatformPosition(Platform lastPlatform, bool isGemSpawn)
    {
        // Get details from the last *platform*
        Platform lastPlatformScript = lastPlatform.GetComponent<Platform>();
        float lastPlatformWidth = lastPlatformScript.GetWidth();
        Vector2 lastPlatformPos = lastPlatform.transform.position;
        float lastPlatformRightEdgeX = lastPlatformPos.x + lastPlatformWidth / 2;

        // --- Decide on vertical position ---
        float yChange = Random.Range(minYDistance, maxYDistance);
        float nextY = Mathf.Clamp(lastPlatformPos.y + yChange, owner.minYHeight, owner.maxYHeight);

        Vector2 nextPlatformSpawnPos;

        if (isGemSpawn)
        {
            // *** GEM SPAWN LOGIC (Based on your specific rule) ***

            // 1. Calculate Gem Position
            // X: (Right edge of last platform) + (max distance)
            float gemX = lastPlatformRightEdgeX + maxXDistance;
            // Y: Vertically halfway between the last platform and the next one
            float gemY = (lastPlatformPos.y + nextY) / 2;

            owner.SpawnGemAt(new Vector2(gemX, gemY));

            // 2. Calculate Next Platform Position
            // Its *center* is at: (Gem's X) + (min distance * 1.2) + (half width of new platform)
            float nextPlatformCenterX = gemX + minXDistance;
            nextPlatformSpawnPos = new Vector2(nextPlatformCenterX, nextY);

            owner.didSpawnGemLast = true; // Mark that we just spawned a gem
        }
        else
        {
            // *** NO GEM SPAWN LOGIC ***

            // 1. Calculate random horizontal gap
            float xGap = Random.Range(minXDistance, maxXDistance);

            // 2. Calculate Next Platform Position
            // Its *center* is at: (Right edge of last platform) + (gap) + (half width of new platform)
            float nextPlatformCenterX = lastPlatformRightEdgeX + xGap;
            nextPlatformSpawnPos = new Vector2(nextPlatformCenterX, nextY);

            owner.didSpawnGemLast = false; // Mark that we did not spawn a gem
        }
        return nextPlatformSpawnPos;
    }
}
