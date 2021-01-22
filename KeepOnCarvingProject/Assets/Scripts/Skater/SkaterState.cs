using System.Collections;
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

    private void Update()
    {
        var prevState = currentState;
        if (SkaterInput.Ollie() && ((currentState & SkaterOllie.COMPATIBLE_STATES) == 1))
        {
            currentState = SKATER_STATE_OLLIE;
            StartCoroutine(DoActionThenResetState(ollieBehaviour.Ollie(), prevState));
        }
        if ((SkaterInput.Down() || SkaterInput.Up()) && (currentState & LaneTransition.COMPATIBLE_STATES) == 1)
        {
            currentState = SKATER_STATE_LANE_CHANGE;
            StartCoroutine(DoActionThenResetState(laneTransitionBehaviour.ChangeLane(), prevState));
        }
    }

    private IEnumerator DoActionThenResetState(IEnumerator routine, int resetState)
    {
        yield return StartCoroutine(routine);

        currentState = resetState;
    }
}
