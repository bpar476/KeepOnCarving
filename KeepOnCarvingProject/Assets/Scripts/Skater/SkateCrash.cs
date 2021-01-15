using System.Collections;
using UnityEngine;

public class SkateCrash : MonoBehaviour
{
    [SerializeField]
    private float crashDuration;

    [SerializeField]
    private SharedFloat skaterSpeed;

    [SerializeField]
    private EventBusContainer busContainer;

    private EventBus eventBus;

    private void Awake()
    {
        eventBus = busContainer.Bus;
    }

    public void Crash()
    {
        eventBus.Raise<SkaterCrashEvent>(new SkaterCrashEvent());
        StartCoroutine(StopSkaterGradually());
    }

    private IEnumerator StopSkaterGradually()
    {
        var currentSpeed = skaterSpeed.Value;
        for (float i = 0; i < crashDuration; i += Time.fixedDeltaTime)
        {
            skaterSpeed.Value = Mathf.Lerp(currentSpeed, 0, i / crashDuration);
            yield return new WaitForFixedUpdate();
        }

        skaterSpeed.Value = 0;
    }
}
