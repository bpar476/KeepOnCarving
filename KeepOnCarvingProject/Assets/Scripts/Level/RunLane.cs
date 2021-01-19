using UnityEngine;

public class RunLane : MonoBehaviour
{
    private static readonly string MAT_PROPERTY_OFFSET = "_InitialOffset";
    private static readonly string MAT_PROPERTY_SCROLL_SPEED = "_ScrollSpeed";

    [SerializeField]
    private RunLanesConfiguration configuration;

    [SerializeField]
    private RunLanesConfiguration.Lane lane;

    [SerializeField]
    private SharedFloat skaterSpeed;

    private Material mat;
    private float cachedSpeed;

    private void Awake()
    {
        var data = GetRunLaneDataForThisLane();
        transform.position = new Vector3(transform.position.x, data.WorldYPosition);

        mat = GetComponent<SpriteRenderer>().material;
        mat.SetFloat(MAT_PROPERTY_OFFSET, (float)Random.Range(0, 320));
        Debug.Log(mat.GetFloat(MAT_PROPERTY_OFFSET));
        UpdatePathScrollSpeed();
    }

    private void Update()
    {
        if (skaterSpeed != cachedSpeed)
        {
            UpdatePathScrollSpeed();
        }
    }

    private void UpdatePathScrollSpeed()
    {
        Debug.Log(skaterSpeed / 25f);
        mat.SetFloat(MAT_PROPERTY_SCROLL_SPEED, skaterSpeed / 25f);
        cachedSpeed = skaterSpeed.Value;
    }

    private void OnDrawGizmos()
    {
        var data = GetRunLaneDataForThisLane();
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(new Vector3(-1, data.WorldYPosition, 0), 0.2f);
    }

    private RunLaneData GetRunLaneDataForThisLane()
    {
        return lane == RunLanesConfiguration.Lane.Top ? configuration.Top : configuration.Bottom;
    }
}
