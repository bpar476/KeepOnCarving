using UnityEngine;

public class SkaterInput
{
    public static bool Up()
    {
        return Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W);
    }

    public static bool Down()
    {
        return Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S);
    }

    public static bool Ollie()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
}
