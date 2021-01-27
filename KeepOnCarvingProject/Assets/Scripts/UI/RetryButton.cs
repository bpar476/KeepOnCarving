using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RetryButton : MonoBehaviour
{
    [SerializeField]
    private EventBusContainer busContainer;
    private Button retryButton;

    private EventBus bus;

    private Guid crashEventHandlerGuid;

    private void Awake()
    {

        retryButton = GetComponentInChildren<Button>();
        retryButton.onClick.AddListener(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));
        retryButton.gameObject.SetActive(false);
    }

    private void Start()
    {
        bus = busContainer.Bus;

        crashEventHandlerGuid = bus.ListenTo<SkaterCrashEvent>(_ =>
            {
                retryButton.gameObject.SetActive(true);
            }
        );
    }

    private void OnDestroy()
    {
        bus.UnsubscribeFrom<SkaterCrashEvent>(crashEventHandlerGuid);
    }
}
