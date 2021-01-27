using UnityEngine;

[CreateAssetMenu(fileName = "HighScoreTracker", menuName = "KeepOnCarving/High Score Tracker")]
public class HighScoreTracker : ScriptableObject
{
    private static readonly string PLAYER_PREF_KEY_HIGH_SCORE = "highScore";

    public int HighScore { get; private set; }

    [SerializeField]
    private EventBusContainer busContainer;

    [SerializeField]
    private SharedFloat skaterScore;

    private System.Guid eventListenerId;

    private void OnEnable()
    {
        HighScore = PlayerPrefs.GetInt(PLAYER_PREF_KEY_HIGH_SCORE, 0);
        var eventListenerId = busContainer.Bus.ListenTo<SkaterCrashEvent>(SetNewHighScore);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetInt(PLAYER_PREF_KEY_HIGH_SCORE, HighScore);
        busContainer.Bus.UnsubscribeFrom<SkaterCrashEvent>(eventListenerId);
    }

    private void SetNewHighScore(SkaterCrashEvent _)
    {
        var score = (int)skaterScore.Value;
        if (score > HighScore)
        {
            HighScore = score;
            busContainer.Bus.Raise<NewHighScoreEvent>(new NewHighScoreEvent(HighScore));
        }
    }
}
