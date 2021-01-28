using UnityEngine;

public class OpenURLBehaviour : MonoBehaviour
{
    public void OpenURL(string URL)
    {
        Application.OpenURL(URL);
    }
}
