using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(SpriteRenderer))]
public class CollectCoin : MonoBehaviour
{

    [SerializeField]
    private int scoreValue;

    [SerializeField]
    private SharedFloat skaterScore;

    private new AudioSource audio;

    private new SpriteRenderer renderer;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
        renderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.IsPlayer())
        {
            skaterScore.Value += scoreValue;
            audio.Play();
            renderer.enabled = false;
        }
    }
}
