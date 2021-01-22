using UnityEngine;

[RequireComponent(typeof(SkaterOllie), typeof(LaneTransition))]
public class SkaterState : MonoBehaviour
{

    public static readonly int SKATER_STATE_ROLLING = 1;
    public static readonly int SKATER_STATE_OLLIE = 1 << 1;
    public static readonly int SKATER_STATE_LANE_CHANGE = 1 << 2;

    private int currentState;

    private SkaterOllie ollieBehaviour;
    private LaneTransition laneTransitionBehaviour;

    private void Awake()
    {
        ollieBehaviour = GetComponent<SkaterOllie>();
        laneTransitionBehaviour = GetComponent<LaneTransition>();
        currentState = SKATER_STATE_ROLLING;
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.LogFormat("State rolling: {0}", SKATER_STATE_ROLLING);
        Debug.LogFormat("State ollie: {0}", SKATER_STATE_OLLIE);
        Debug.LogFormat("State change lane: {0}", SKATER_STATE_LANE_CHANGE);
    }

    private void Update()
    {
        if (SkaterInput.Ollie() && ((currentState & SkaterOllie.COMPATIBLE_STATES) == 1))
        {
            ollieBehaviour.Ollie();
        }
        if ((SkaterInput.Down() || SkaterInput.Up()) && (currentState & LaneTransition.COMPATIBLE_STATES) == 1)
        {
            laneTransitionBehaviour.ChangeLane();
        }
    }

    public void SetState(int state)
    {
        currentState = state;
    }
}
