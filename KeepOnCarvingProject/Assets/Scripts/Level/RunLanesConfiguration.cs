using UnityEngine;

[CreateAssetMenu(fileName = "RunLanesConfiguration", menuName = "KeepOnCarving/RunLanesConfiguration", order = 51)]
public class RunLanesConfiguration : ScriptableObject
{
    public RunLaneData Top;

    public RunLaneData Bottom;

    public enum Lane
    {
        Top, Bottom
    }
}
