using System.Collections;
using UnityEngine;

public class SkaterOllie : MonoBehaviour
{
    private static readonly string ANIM_BOOL_OLLIE = "ollie";

    [SerializeField]
    private float jumpDistance;

    [SerializeField]
    private float jumpHeight;

    [SerializeField]
    private SharedFloat skaterDistance;

    private Animator animator;
    private bool jumping = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !jumping)
        {
            StartCoroutine(Ollie());
        }
    }

    private IEnumerator Ollie()
    {
        animator.SetBool(ANIM_BOOL_OLLIE, true);
        jumping = true;
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
        jumping = false;
        animator.SetBool(ANIM_BOOL_OLLIE, false);
    }
}
