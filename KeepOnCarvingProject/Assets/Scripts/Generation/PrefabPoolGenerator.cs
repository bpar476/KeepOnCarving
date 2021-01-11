using UnityEngine;

public class PrefabPoolGenerator : MonoBehaviour
{
    /// <summary>
    /// The pool of objects that can be spawned by the generator;
    /// </summary>
    [SerializeField]
    private GameObject[] pool;

    public GameObject Spawn(Vector2 position)
    {
        if (pool.Length == 0)
        {
            throw new System.Exception("Object pool is empty, can't spawn object");
        }
        // Get a random integer from uniform distribution between 0 and last index of object pool.
        var idx = (int)Mathf.Floor(Random.Range(0, pool.Length - 0.01f));
        return Instantiate(pool[idx], position, Quaternion.identity);
    }
}
