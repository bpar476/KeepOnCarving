using UnityEngine;

public class RunLane : MonoBehaviour
{
    private static readonly string MAT_PROPERTY_SCROLL = "_ScrollOffset";

    [SerializeField]
    private RunLanesConfiguration configuration;

    [SerializeField]
    private RunLanesConfiguration.Lane lane;

    [SerializeField]
    private SharedFloat skaterDistance;

    private Material mat;

    private float offset;

    private void Awake()
    {
        var data = GetRunLaneDataForThisLane();
        transform.position = new Vector3(transform.position.x, data.WorldYPosition);

        mat = GetComponent<SpriteRenderer>().material;
        offset = Random.Range(0, 100) / 100f;
        UpdatePathScroll();
    }

    private void Update()
    {
        UpdatePathScroll();
    }

    private void UpdatePathScroll()
    {
        mat.SetFloat(MAT_PROPERTY_SCROLL, skaterDistance * 0.05f + offset);
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
