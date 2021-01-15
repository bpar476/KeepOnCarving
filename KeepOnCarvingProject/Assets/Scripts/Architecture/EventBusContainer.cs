using UnityEngine;

[CreateAssetMenu(fileName = "EventBus", menuName = "KeepOnCarving/EventBusContainer", order = 53)]
public class EventBusContainer : ScriptableObject
{

    public EventBus Bus { get; private set; }

    private void OnEnable()
    {
        Bus = new EventBus();
    }

}
