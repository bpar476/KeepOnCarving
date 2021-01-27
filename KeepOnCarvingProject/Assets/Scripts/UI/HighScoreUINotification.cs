using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreUINotification : MonoBehaviour
{
    private static readonly string ANIM_TRIGGER_FLASH = "newHighScore";

    [SerializeField]
    private EventBusContainer busContainer;

    private Animator animator;

    private System.Guid eventListenerGuid;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        eventListenerGuid = busContainer.Bus.ListenTo<NewHighScoreEvent>(FlashHighScoreUI);
    }

    private void OnDestroy()
    {
        if (busContainer != null && busContainer.Bus != null)
        {
            busContainer.Bus.UnsubscribeFrom<NewHighScoreEvent>(eventListenerGuid);
        }
    }

    private void FlashHighScoreUI(NewHighScoreEvent _)
    {
        animator.SetTrigger(ANIM_TRIGGER_FLASH);
    }
}
