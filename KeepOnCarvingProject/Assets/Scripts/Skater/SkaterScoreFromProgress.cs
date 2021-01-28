using UnityEngine;

public class SkaterScoreFromProgress : MonoBehaviour
{
    [SerializeField]
    private SharedFloat skaterDistance;

    [SerializeField]
    private SharedFloat skaterScore;

    [SerializeField]
    private EventBusContainer busContainer;

    private int cachedDistance = 0;

    private System.Guid eventListenerToken;

    private void Start()
    {
        eventListenerToken = busContainer.Bus.ListenTo<RetryEvent>(_ => cachedDistance = 0);
    }

    private void Update()
    {
        var distance = (int)skaterDistance.Value;
        if (distance > cachedDistance)
        {
            skaterScore.Value += 1;
            cachedDistance = distance;
        }
    }

    private void OnDestroy()
    {
        if (busContainer.Bus != null)
        {
            busContainer.Bus.UnsubscribeFrom<RetryEvent>(eventListenerToken);
        }
    }
}
