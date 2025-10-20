using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData", order = 1)]
public class LevelData : ScriptableObject
{
    public float moveSpeed = 5f;
    public float destroyXPosition = -10f;

    public float minXDistance = 2f;
    public float maxXDistance = 5f;
    public float minYDistance = -1f;
    public float maxYDistance = 1f;

    public Vector2 initialSpawnPos = new Vector2(10f, 0f);
}
