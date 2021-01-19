using UnityEngine;

public class RunLaneGenerator : RandomDistanceSpawner
{
    [SerializeField]
    private RunLanesConfiguration lanesConfiguration;

    [SerializeField]
    private RunLanesConfiguration.Lane lane;

    private RunLaneData runLane;

    private void Awake()
    {
        runLane = lane == RunLanesConfiguration.Lane.Bottom ? lanesConfiguration.Bottom : lanesConfiguration.Top;
        transform.position = new Vector3(0, runLane.WorldYPosition, 0);
    }

    public override GameObject Spawn()
    {
        var obj = base.Spawn();
        obj.layer = LayerMask.NameToLayer(runLane.CollisionLayer);
        return obj;
    }
}
