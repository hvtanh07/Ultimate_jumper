using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterEvent events;
    public CharacterScriptableStats stats;

    /// <summary>
    /// All the character states can be stored here
    /// </summary>
    public bool isJumping = false;
    public bool isGrounded = true;
    public bool isDead = false;
}
