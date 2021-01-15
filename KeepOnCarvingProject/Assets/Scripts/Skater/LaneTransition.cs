using UnityEngine;
using System.Collections;

public class LaneTransition : MonoBehaviour
{

    private static readonly string ANIM_BOOL_LANE_UP = "ChangeLaneUp";
    private static readonly string ANIM_BOOL_LANE_DOWN = "ChangeLaneDown";

    [SerializeField]
    private RunLanesConfiguration lanesConfiguration;

    [SerializeField]
    private float transitionDuration;

    private Animator animator;

    private bool changingLanes = false;
    private bool inTopLane;
    private float meanLanePosition;

    private void Awake()
    {
        animator = GetComponent<Animator>();
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
                animator.SetBool(ANIM_BOOL_LANE_DOWN, true);
                SwitchToLane(lanesConfiguration.Bottom);
            }

            if (SkaterInput.Up() && !inTopLane)
            {
                animator.SetBool(ANIM_BOOL_LANE_UP, true);
                SwitchToLane(lanesConfiguration.Top);
            }
        }
    }

    private void SwitchToLane(RunLane lane)
    {
        changingLanes = true;
        StartCoroutine(SmoothTransitionToLane(lane));
    }

    private IEnumerator SmoothTransitionToLane(RunLane lane)
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

    private void HardSetLane(RunLane lane)
    {
        transform.position = new Vector2(transform.position.x, lane.WorldYPosition);
        gameObject.layer = LayerMask.NameToLayer(lane.CollisionLayer);
    }

}
