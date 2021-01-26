using UnityEngine;

public class SkaterScoreFromProgress : MonoBehaviour
{
    [SerializeField]
    private SharedFloat skaterDistance;

    [SerializeField]
    private SharedFloat skaterScore;

    private int cachedDistance = 0;

    private void Update()
    {
        var distance = (int)skaterDistance.Value;
        if (distance > cachedDistance)
        {
            skaterScore.Value += 1;
            cachedDistance = distance;
        }
    }
}
