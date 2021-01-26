using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(SkaterSoundEffects), typeof(SkaterState))]
public class SkaterOllie : MonoBehaviour
{
    public static readonly int COMPATIBLE_STATES = SkaterState.SKATER_STATE_ROLLING;
    private static readonly string ANIM_BOOL_OLLIE = "ollie";

    [SerializeField]
    private float jumpTime;

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
        var currentTime = Time.fixedTime;
        var initialY = transform.position.y;
        var firstJumpHalf = jumpTime / 2;
        var secondJumpHalf = jumpTime - firstJumpHalf;

        // Rise
        for (; Time.fixedTime < currentTime + firstJumpHalf;)
        {
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(initialY, initialY + jumpHeight, (Time.fixedTime - currentTime) / (firstJumpHalf)));
            yield return new WaitForFixedUpdate();
        }

        currentTime = Time.fixedTime;
        // Fall
        for (; Time.fixedTime < currentTime + secondJumpHalf;)
        {
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(initialY + jumpHeight, initialY, (Time.fixedTime - currentTime) / (secondJumpHalf)));
            yield return new WaitForFixedUpdate();
        }

        transform.position = new Vector3(transform.position.x, initialY);
        animator.SetBool(ANIM_BOOL_OLLIE, false);
        sfx.PlayLandSoundEffect();
        sfx.PlayRollSoundEffect();
    }
}
