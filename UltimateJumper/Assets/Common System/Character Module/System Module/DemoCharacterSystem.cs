using UnityEngine;


public class DemoCharacterSystem : CharacterSystem
{
    //Set it own parameters here
    protected override void Awake()
    {
        base.Awake();
    }
    void OnEnable()
    {
        //If some event is raise then catch that event to handle
        //character.events.On += HandleEvent; // Subscribe to the OnAlerted event
    }
    void OnDisable()
    {
        //If some event is raise then catch that event to handle
        //character.events.On -= HandleEvent; // Unsubscribe from the OnAlerted event
    }

    private void HandleEvent()
    {
        //Check for params
        //character.isgrounded = true;

        //Get characeter stats
        //character.stats.health;
        
        //Raise an event for other component to catch
        //character.events.OnEvent?.Invoke();
    }
}
