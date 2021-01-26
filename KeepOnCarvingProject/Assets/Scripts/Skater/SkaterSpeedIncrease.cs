using UnityEngine;

public class SkaterSpeedIncrease : MonoBehaviour
{
    [SerializeField]
    private SharedFloat skaterScore;

    [SerializeField]
    private SharedFloat skaterSpeed;

    /// <summary>
    /// The amount of score the skater must achieve between increases in skater speed
    /// </summary>
    [SerializeField]
    private int speedIncreaseScore;

    /// <summary>
    /// The amount that the skater increases in speed after travelling the Speed Increase Distance
    /// </summary>
    [SerializeField]
    private float speedIncreaseAmount;

    private float nextIncreaseScore;

    private void Awake()
    {
        nextIncreaseScore = speedIncreaseScore;
    }

    void Update()
    {
        if (skaterScore > nextIncreaseScore)
        {
            nextIncreaseScore += speedIncreaseScore;
            skaterSpeed.Value += speedIncreaseAmount;
        }
    }
}
