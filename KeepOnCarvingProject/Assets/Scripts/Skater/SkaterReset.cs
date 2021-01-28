using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(SkaterSoundEffects))]
public class SkaterReset : MonoBehaviour
{
    private static readonly string ANIM_TRIGGER_RETRY = "retry";

    [SerializeField]
    private SharedFloat skaterDistance;

    [SerializeField]
    private SharedFloat skaterScore;

    [SerializeField]
    private SharedFloat skaterSpeed;

    private Animator animator;

    private SkaterSoundEffects sfx;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        sfx = GetComponent<SkaterSoundEffects>();
    }

    public void Reset()
    {
        skaterDistance.Value = 0;
        skaterScore.Value = 0;
        skaterSpeed.Value = skaterSpeed.DefaultValue;

        animator.SetTrigger(ANIM_TRIGGER_RETRY);
        sfx.PlayRollSoundEffect();
    }

}
