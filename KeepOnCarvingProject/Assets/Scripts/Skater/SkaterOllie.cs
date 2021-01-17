using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkaterOllie : MonoBehaviour
{
    [SerializeField]
    private float jumpDistance;

    [SerializeField]
    private float jumpHeight;

    [SerializeField]
    private SharedFloat skaterDistance;

    private bool jumping = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !jumping)
        {
            StartCoroutine(Ollie());
        }
    }

    private IEnumerator Ollie()
    {
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
    }
}
