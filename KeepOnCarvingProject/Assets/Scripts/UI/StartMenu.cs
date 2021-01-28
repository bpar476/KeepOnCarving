using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class StartMenu : MonoBehaviour
{
    [SerializeField]
    private SharedFloat skaterSpeed;

    [SerializeField]
    private EventBusContainer busContainer;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        skaterSpeed.Value = 0;
    }

    public void StartRun()
    {
        skaterSpeed.Value = skaterSpeed.DefaultValue;
        gameObject.SetActive(false);
        audioSource.Stop();
        busContainer.Bus.Raise<GameStartEvent>(new GameStartEvent());
    }
}
