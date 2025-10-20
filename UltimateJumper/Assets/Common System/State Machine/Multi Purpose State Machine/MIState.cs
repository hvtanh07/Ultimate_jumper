using UnityEditorInternal;
using UnityEngine;

/// <summary>
/// The base interface for all states in the state machine. "T" will be replaced by the context type (e.g., PlayerController, UIManager).
/// </summary>
/// <typeparam name="T">The type of the context object this state belongs to.</typeparam>
public interface MIState<T>
{
    /// <summary>
    /// Called once when the state is first entered.
    /// Used for setup and initialization.
    /// </summary>
    void OnEnter(T owner);

    /// <summary>
    /// Called every frame while this state is active.
    /// Used for game logic and checking transition conditions.
    /// </summary>
    void OnUpdate();

    /// <summary>
    /// Called once when the state is exited.
    /// Used for cleanup.
    /// </summary>
    void OnExit();
}
