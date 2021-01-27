using UnityEngine;

public class HighScoreUIUpdater : MonoBehaviour
{
    [SerializeField]
    private EventBusContainer busContainer;

    [SerializeField]
    private HighScoreTracker highScoreTracker;

    private TMPro.TMP_Text text;

    private System.Guid eventListenerGuid;

    private void Awake()
    {
        text = GetComponent<TMPro.TMP_Text>();
    }

    private void Start()
    {
        eventListenerGuid = busContainer.Bus.ListenTo<NewHighScoreEvent>(UpdateHighScoreUI);
        text.text = highScoreTracker.HighScore.ToString();
    }

    private void OnDestroy()
    {
        busContainer.Bus.UnsubscribeFrom<NewHighScoreEvent>(eventListenerGuid);
    }

    private void UpdateHighScoreUI(NewHighScoreEvent scoreEvent)
    {
        text.text = scoreEvent.Score.ToString();
    }

}
