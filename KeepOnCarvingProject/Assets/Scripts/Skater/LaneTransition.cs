using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator), typeof(SkaterSoundEffects), typeof(SkaterState))]
public class LaneTransition : MonoBehaviour
{

    public static readonly int COMPATIBLE_STATES = SkaterState.SKATER_STATE_ROLLING;

    private static readonly string ANIM_BOOL_LANE_UP = "ChangeLaneUp";
    private static readonly string ANIM_BOOL_LANE_DOWN = "ChangeLaneDown";

    [SerializeField]
    private RunLanesConfiguration lanesConfiguration;

    [SerializeField]
    private float transitionDuration;

    private Animator animator;

    private SkaterSoundEffects sfx;

    private SkaterState state;

    private bool inTopLane;
    private float meanLanePosition;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        sfx = GetComponent<SkaterSoundEffects>();
        state = GetComponent<SkaterState>();
    }

    private void Start()
    {
        HardSetLane(lanesConfiguration.Bottom);
        inTopLane = false;
        meanLanePosition = (lanesConfiguration.Bottom.WorldYPosition + lanesConfiguration.Top.WorldYPosition) / 2;
    }

    public void ChangeLane()
    {
        if (SkaterInput.Down() && inTopLane)
        {
            // Change down lane
            animator.SetBool(ANIM_BOOL_LANE_DOWN, true);
            sfx.PlayLaneDownSoundEffect();
            SwitchToLane(lanesConfiguration.Bottom);
        }

        if (SkaterInput.Up() && !inTopLane)
        {
            // Chane up lane
            animator.SetBool(ANIM_BOOL_LANE_UP, true);
            sfx.PlayLaneUpSoundEffect();
            SwitchToLane(lanesConfiguration.Top);
        }
    }

    private void SwitchToLane(RunLaneData lane)
    {
        state.SetState(SkaterState.SKATER_STATE_LANE_CHANGE);
        StartCoroutine(SmoothTransitionToLane(lane));
    }

    private IEnumerator SmoothTransitionToLane(RunLaneData lane)
    {
        float initialYPos = transform.position.y;
        for (float i = 0; i < transitionDuration; i += Time.fixedDeltaTime)
        {
            transform.position = new Vector2(transform.position.x, Mathf.Lerp(initialYPos, lane.WorldYPosition, i / transitionDuration));
            if ((transform.position.y > meanLanePosition && !inTopLane) || (transform.position.y < meanLanePosition && inTopLane))
            {
                gameObject.layer = LayerMask.NameToLayer(lane.CollisionLayer);
                inTopLane = !inTopLane;
            }
            yield return new WaitForFixedUpdate();
        }

        HardSetLane(lane);
    }

    private void HardSetLane(RunLaneData lane)
    {
        transform.position = new Vector2(transform.position.x, lane.WorldYPosition);
        gameObject.layer = LayerMask.NameToLayer(lane.CollisionLayer);
        animator.SetBool(ANIM_BOOL_LANE_DOWN, false);
        animator.SetBool(ANIM_BOOL_LANE_UP, false);
        state.SetState(SkaterState.SKATER_STATE_ROLLING);
    }

}
