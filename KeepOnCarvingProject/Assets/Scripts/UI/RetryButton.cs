using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RetryButton : MonoBehaviour
{
    [SerializeField]
    private EventBusContainer busContainer;

    [SerializeField]
    private SharedFloat skaterDistance;

    [SerializeField]
    private SharedFloat skaterScore;

    private Button retryButton;

    private EventBus bus;

    private Guid crashEventHandlerGuid;

    private void Awake()
    {
        retryButton = GetComponentInChildren<Button>();
        retryButton.onClick.AddListener(Retry);
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

    private void Retry()
    {
        bus.Raise<RetryEvent>(new RetryEvent());
        retryButton.gameObject.SetActive(false);
        skaterDistance.Value = 0;
    }

    private void OnDestroy()
    {
        bus.UnsubscribeFrom<SkaterCrashEvent>(crashEventHandlerGuid);
    }
}
