using UnityEngine;


public class CharacterSystem : MonoBehaviour
{
    protected Character character;
    protected virtual void Awake()
    {
        character = transform.root.GetComponent<Character>();

        if (character == null)
        {
            Debug.LogWarning("Parent of " + gameObject.name + " is missing the main Character component.");
        }
    }
}

