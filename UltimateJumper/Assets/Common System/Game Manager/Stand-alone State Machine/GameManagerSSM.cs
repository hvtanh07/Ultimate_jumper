using Unity.VisualScripting;
using UnityEngine;

public class GameManagerSSM : MonoBehaviour
{

    public static GameManagerSSM Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        // Get the StateManager component.
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

    public StateManagerGM stateMachine;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StateManagerGM stateManager = new StateManagerGM();
        stateManager.Initialize(new NewGameSimpleState());
    }
}
