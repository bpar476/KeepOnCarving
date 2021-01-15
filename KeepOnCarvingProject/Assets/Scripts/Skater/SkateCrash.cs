using System.Collections;
using UnityEngine;

public class SkateCrash : MonoBehaviour
{
    [SerializeField]
    private float crashDuration;

    [SerializeField]
    private SharedFloat skaterSpeed;

    [SerializeField]
    private Animator skateBoardAnimator;

    [SerializeField]
    private EventBusContainer busContainer;

    private Animator animator;

    private EventBus eventBus;

    private void Awake()
    {
        eventBus = busContainer.Bus;
        animator = GetComponent<Animator>();
    }

    public void Crash()
    {
        eventBus.Raise<SkaterCrashEvent>(new SkaterCrashEvent());
        skaterSpeed.Value = 0;
        animator.SetTrigger("crash");
        skateBoardAnimator.speed = 0;
    }
}
