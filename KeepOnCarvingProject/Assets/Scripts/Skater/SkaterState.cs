using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SkaterOllie), typeof(LaneTransition), typeof(SkaterReset))]
public class SkaterState : MonoBehaviour
{
    public static readonly int SKATER_STATE_ROLLING = 1;
    public static readonly int SKATER_STATE_OLLIE = 1 << 1;
    public static readonly int SKATER_STATE_LANE_CHANGE = 1 << 2;

    [SerializeField]
    private EventBusContainer busContainer;

    private Animator animator;
    private int currentState;

    private SkaterOllie ollieBehaviour;
    private SkaterReset resetBehaviour;
    private LaneTransition laneTransitionBehaviour;

    private void Awake()
    {
        ollieBehaviour = GetComponent<SkaterOllie>();
        laneTransitionBehaviour = GetComponent<LaneTransition>();
        resetBehaviour = GetComponent<SkaterReset>();
        currentState = SKATER_STATE_ROLLING;
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        busContainer.Bus.ListenTo<RetryEvent>((_) =>
        {
            currentState = SKATER_STATE_ROLLING;
            resetBehaviour.Reset();
        });
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
