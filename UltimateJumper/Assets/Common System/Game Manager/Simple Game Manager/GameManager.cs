using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public int Score { get; private set; }
    public int Lives { get; private set; } = 3;
    public bool IsGameOver { get; private set; }

    public void AddScore(int amount)
    {
        if (IsGameOver) return;
        Score += amount;
        // Optionally, notify listeners about score change
    }

    public void LoseLife()
    {
        if (IsGameOver) return;
        Lives--;
        if (Lives <= 0)
        {
            GameOver();
        }
        // Optionally, notify listeners about life change
    }

    public void RestartGame()
    {
        Score = 0;
        Lives = 3;
        IsGameOver = false;
        // Optionally, reload the current scene or reset game state
    }

    private void GameOver()
    {
        IsGameOver = true;
        // Optionally, trigger game over UI or logic
    }
}
