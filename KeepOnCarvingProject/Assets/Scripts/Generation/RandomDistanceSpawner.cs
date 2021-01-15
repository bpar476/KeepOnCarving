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

    private float nextSpawnLocation;

    private void Start()
    {
        nextSpawnLocation = 0;
        CalculateNextSpawnLocation();
    }

    void Update()
    {
        if (skaterDistance > nextSpawnLocation)
        {
            Spawn();
            CalculateNextSpawnLocation();
        }
    }

    public virtual GameObject Spawn()
    {
        return generator.Spawn(new Vector2(nextSpawnLocation + offscreenOffset, transform.position.y));
    }

    private void CalculateNextSpawnLocation()
    {
        var prevSpawnLocation = nextSpawnLocation;
        var distance = Random.Range(minDistance, maxDistance);
        nextSpawnLocation = prevSpawnLocation + distance;
    }
}
