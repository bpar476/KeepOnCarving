using UnityEngine;

[CreateAssetMenu(fileName = "RunLanesConfiguration", menuName = "KeepOnCarving/RunLanesConfiguration", order = 51)]
public class RunLanesConfiguration : ScriptableObject
{
    public RunLane Top;

    public RunLane Bottom;
}
