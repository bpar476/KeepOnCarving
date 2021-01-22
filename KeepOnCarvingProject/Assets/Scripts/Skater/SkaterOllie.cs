using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(SkaterSoundEffects), typeof(SkaterState))]
public class SkaterOllie : MonoBehaviour
{
    public static readonly int COMPATIBLE_STATES = SkaterState.SKATER_STATE_ROLLING;
    private static readonly string ANIM_BOOL_OLLIE = "ollie";

    [SerializeField]
    private float jumpDistance;

    [SerializeField]
    private float jumpHeight;

    [SerializeField]
    private SharedFloat skaterDistance;

    private SkaterSoundEffects sfx;

    private SkaterState state;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        sfx = GetComponent<SkaterSoundEffects>();
        state = GetComponent<SkaterState>();
    }

    private void Start()
    {
        sfx.PlayRollSoundEffect();
    }

    public IEnumerator Ollie()
    {
        sfx.PlayOllieSoundEffect();
        return DoOllie();
    }

    private IEnumerator DoOllie()
    {
        animator.SetBool(ANIM_BOOL_OLLIE, true);
        var currentDistance = skaterDistance.Value;
        var initialY = transform.position.y;
        var firstJumpHalf = jumpDistance / 2;
        var secondJumpHalf = jumpDistance - firstJumpHalf;

        // Rise
        for (; skaterDistance.Value < currentDistance + firstJumpHalf;)
        {
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(initialY, initialY + jumpHeight, (skaterDistance.Value - currentDistance) / (firstJumpHalf)));
            yield return new WaitForFixedUpdate();
        }

        currentDistance = skaterDistance.Value;
        // Fall
        for (; skaterDistance.Value < currentDistance + secondJumpHalf;)
        {
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(initialY + jumpHeight, initialY, (skaterDistance.Value - currentDistance) / (secondJumpHalf)));
            yield return new WaitForFixedUpdate();
        }

        transform.position = new Vector3(transform.position.x, initialY);
        animator.SetBool(ANIM_BOOL_OLLIE, false);
        sfx.PlayLandSoundEffect();
        sfx.PlayRollSoundEffect();
    }
}
