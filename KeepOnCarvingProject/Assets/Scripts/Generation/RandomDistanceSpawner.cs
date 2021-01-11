using UnityEngine;

public class RandomDistanceSpawner : MonoBehaviour
{
    /// <summary>
    /// The minimum distance the skater should travel before a new object can spawn
    /// </summary>
    [SerializeField]
    private float minDistance;

    /// <summary>
    /// The maximum distance the skater should travel before a new object will be spawned
    /// </summary>
    [SerializeField]
    private float maxDistance;

    [SerializeField]
    private PrefabPoolGenerator generator;

    [SerializeField]
    private SharedFloat skaterDistance;

    /// <summary>
    /// Amount to offset the position of the spawned object by;
    /// </summary>
    [SerializeField]
    private float offscreenOffset;

    private float nextSpawnLocation;

    private void Start()
    {
        CalculateNextSpawnLocation();
    }

    // Update is called once per frame
    void Update()
    {
        if (skaterDistance > nextSpawnLocation)
        {
            generator.Spawn(new Vector2(nextSpawnLocation + offscreenOffset, transform.position.y));
            CalculateNextSpawnLocation();
        }
    }

    private void CalculateNextSpawnLocation()
    {
        var distance = Random.Range(minDistance, maxDistance);
        nextSpawnLocation = skaterDistance + distance;
    }
}
