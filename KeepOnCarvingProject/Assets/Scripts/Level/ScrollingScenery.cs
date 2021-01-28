using UnityEngine;

public class ScrollingScenery : MonoBehaviour
{

    [SerializeField]
    private SharedFloat skaterDistance;

    [SerializeField]
    private float parallaxFactor = 1f;

    [SerializeField]
    private EventBusContainer busContainer;

    private System.Guid eventListenerToken;

    private bool cleanedUp = false;

    private float location;

    private void OnEnable()
    {
        location = transform.position.x;
        busContainer.Bus.ListenTo<RetryEvent>(_ => CleanupObject());
    }

    private void Update()
    {
        var relativeLocation = location - skaterDistance * parallaxFactor;
        transform.position = new Vector2(relativeLocation, transform.position.y);
        if (relativeLocation < -5)
        {
            CleanupObject();
        }
    }

    private void CleanupObject()
    {
        if (!cleanedUp)
        {
            cleanedUp = true;
            busContainer.Bus.UnsubscribeFrom<RetryEvent>(eventListenerToken);
            Destroy(gameObject);
        }
    }

}
