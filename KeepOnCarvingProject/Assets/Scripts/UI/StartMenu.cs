using UnityEngine;

public class StartMenu : MonoBehaviour
{
    [SerializeField]
    private SharedFloat skaterSpeed;

    private void Start()
    {
        skaterSpeed.Value = 0;
    }

    public void StartRun()
    {
        skaterSpeed.Value = skaterSpeed.DefaultValue;
        gameObject.SetActive(false);
    }
}
