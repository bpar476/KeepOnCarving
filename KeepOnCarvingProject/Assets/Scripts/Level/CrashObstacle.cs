using UnityEngine;

public class CrashObstacle : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        var gobj = other.gameObject;
        if (gobj.IsPlayer())
        {
            CrashPlayer();
        }
    }

    private void CrashPlayer()
    {
        Debug.Log("Player crashed");
    }

}
