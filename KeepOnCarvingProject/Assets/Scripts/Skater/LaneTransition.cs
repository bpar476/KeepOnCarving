using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator), typeof(SkaterSoundEffects))]
public class LaneTransition : MonoBehaviour
{

    private static readonly string ANIM_BOOL_LANE_UP = "ChangeLaneUp";
    private static readonly string ANIM_BOOL_LANE_DOWN = "ChangeLaneDown";

    [SerializeField]
    private RunLanesConfiguration lanesConfiguration;

    [SerializeField]
    private float transitionDuration;

    private Animator animator;

    private SkaterSoundEffects sfx;

    private bool changingLanes = false;
    private bool inTopLane;
    private float meanLanePosition;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        sfx = GetComponent<SkaterSoundEffects>();
    }

    private void Start()
    {
        HardSetLane(lanesConfiguration.Bottom);
        inTopLane = false;
        meanLanePosition = (lanesConfiguration.Bottom.WorldYPosition + lanesConfiguration.Top.WorldYPosition) / 2;
    }

    private void Update()
    {
        if (!changingLanes)
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
    }

    private void SwitchToLane(RunLaneData lane)
    {
        changingLanes = true;
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
        changingLanes = false;
        animator.SetBool(ANIM_BOOL_LANE_DOWN, false);
        animator.SetBool(ANIM_BOOL_LANE_UP, false);
    }

    private void HardSetLane(RunLaneData lane)
    {
        transform.position = new Vector2(transform.position.x, lane.WorldYPosition);
        gameObject.layer = LayerMask.NameToLayer(lane.CollisionLayer);
    }

}
