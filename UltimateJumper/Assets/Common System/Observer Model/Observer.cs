using Unity.VisualScripting;
using UnityEngine;

public class Observer : MonoBehaviour
{
    Character character;
    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
    }
    void OnEnable()
    {
        if (character == null) return;
        character.events.OnHealthChange += UpdateHealthBar;
    }
    void OnDisable()
    {
        if (character == null) return;
        character.events.OnHealthChange -= UpdateHealthBar;
    }

    void UpdateHealthBar(int health)
    {
        Debug.Log("Health Changed" + health);
    }
}
