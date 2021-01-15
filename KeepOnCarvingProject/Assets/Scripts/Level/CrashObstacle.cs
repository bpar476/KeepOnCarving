using UnityEngine;

public class CrashObstacle : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        var gobj = other.gameObject;
        if (gobj.IsPlayer())
        {
            CrashPlayer(gobj.GetComponent<SkateCrash>());
        }
    }

    private void CrashPlayer(SkateCrash crashBehaviour)
    {
        crashBehaviour.Crash();
    }

}
