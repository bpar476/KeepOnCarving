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

    private void Awake()
    {
        eventBus = busContainer.Bus;
        animator = GetComponent<Animator>();
        sfx = GetComponent<SkaterSoundEffects>();
    }

    public void Crash()
    {
        eventBus.Raise<SkaterCrashEvent>(new SkaterCrashEvent());
        skaterSpeed.Value = 0;
        animator.SetTrigger("crash");
        sfx.PlayCrashSoundEffect();
    }
}
