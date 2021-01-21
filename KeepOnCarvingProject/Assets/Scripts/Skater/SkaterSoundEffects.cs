using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SkaterSoundEffects : MonoBehaviour
{

    [SerializeField]
    private AudioClip rollingClip;

    [SerializeField]
    private AudioClip ollieClip;

    [SerializeField]
    private AudioClip scoreClip;

    [SerializeField]
    private AudioClip grindClip;

    [SerializeField]
    private AudioClip landClip;

    [SerializeField]
    private AudioClip crashClip;

    [SerializeField]
    private AudioClip laneUpClip;

    [SerializeField]
    private AudioClip laneDownClip;

    private AudioSource source;

    private Queue<(AudioClip, bool)> sfxQueue;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayRollSoundEffect()
    {
        PlayClip(rollingClip, loop: true);
    }

    public void PlayOllieSoundEffect()
    {
        PlayClip(ollieClip, interrupt: true);
    }

    public void PlayScoreSoundEffect()
    {
        PlayClip(scoreClip);
    }

    public void PlayGrindSoundEffect()
    {
        PlayClip(grindClip);
    }

    public void PlayLandSoundEffect()
    {
        PlayClip(landClip);
    }

    public void PlayCrashSoundEffect()
    {
        PlayClip(crashClip);
    }

    public void PlayLaneUpSoundEffect()
    {
        PlayClip(laneUpClip);
    }

    public void PlayLaneDownSoundEffect()
    {
        PlayClip(laneDownClip);
    }

    private void PlayClip(AudioClip clip, bool loop = false, bool interrupt = false)
    {
        if (loop)
        {
            source.clip = clip;
            source.loop = true;
            source.Play();
        }
        else
        {
            // We probably want to cancel a looping sound effect before playing a one shot.
            if (interrupt)
            {
                source.Stop();
            }
            source.PlayOneShot(clip);
        }
    }
}
