using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SkaterSoundEffects), typeof(Animator))]
public class SkateCrash : MonoBehaviour
{
    [SerializeField]
    private float crashDuration;

    [SerializeField]
    private SharedFloat skaterSpeed;

    [SerializeField]
    private EventBusContainer busContainer;

    private SkaterSoundEffects sfx;

    private Animator animator;

    private EventBus eventBus;

    private bool crashed = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        sfx = GetComponent<SkaterSoundEffects>();
        crashed = false;
    }

    private void Start()
    {
        eventBus = busContainer.Bus;
        eventBus.ListenTo<RetryEvent>(_ => crashed = false);
    }

    public void Crash()
    {
        if (!crashed)
        {
            eventBus.Raise<SkaterCrashEvent>(new SkaterCrashEvent());
            skaterSpeed.Value = 0;
            animator.SetTrigger("crash");
            sfx.PlayCrashSoundEffect();
            crashed = true;
        }
    }
}
