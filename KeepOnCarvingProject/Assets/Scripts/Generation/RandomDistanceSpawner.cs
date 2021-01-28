using System.Collections.Generic;
using UnityEngine;

public class RandomDistanceSpawner : MonoBehaviour
{
    /// <summary>
    /// The minimum distance between spawns
    /// </summary>
    [SerializeField]
    private float minDistance;

    /// <summary>
    /// The maximum distance between spawns
    /// </summary>
    [SerializeField]
    private float maxDistance;

    /// <summary>
    ///  Pool of objects that can be spawned
    /// </summary>
    [SerializeField]
    private PrefabPoolGenerator generator;

    [SerializeField]
    private SharedFloat skaterDistance;

    /// <summary>
    /// Amount to offset the position of the spawned object by;
    /// </summary>
    [SerializeField]
    private float offscreenOffset;

    [SerializeField]
    private EventBusContainer busContainer;

    private float nextSpawnLocation;

    private System.Guid eventListenerToken;

    private void Start()
    {
        eventListenerToken = busContainer.Bus.ListenTo<RetryEvent>(_ => Initialise());
    }

    void Update()
    {
        if (skaterDistance > nextSpawnLocation)
        {
            Spawn();
            CalculateNextSpawnLocation();
        }
    }

    private void OnDestroy()
    {
        if (busContainer != null && busContainer.Bus != null)
        {
            busContainer.Bus.UnsubscribeFrom<SkaterCrashEvent>(eventListenerToken);
        }
    }

    public virtual GameObject Spawn()
    {
        return generator.Spawn(new Vector2(nextSpawnLocation + offscreenOffset, transform.position.y));
    }

    private void Initialise()
    {
        nextSpawnLocation = 0;
        CalculateNextSpawnLocation();
    }

    private void CalculateNextSpawnLocation()
    {
        var prevSpawnLocation = nextSpawnLocation;
        var distance = Random.Range(minDistance, maxDistance);
        nextSpawnLocation = prevSpawnLocation + distance;
    }
}
