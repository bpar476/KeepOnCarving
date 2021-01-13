using UnityEngine;

public static class GameObjectExtensions
{
    public static bool IsPlayer(this GameObject gobj)
    {
        return gobj.tag == "Player";
    }
}
