using UnityEngine;

public class HighScoreUI : MonoBehaviour
{
    [SerializeField]
    private HighScoreTracker highScoreTracker;

    private TMPro.TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMPro.TMP_Text>();
    }

    private void Update()
    {
        text.text = highScoreTracker.HighScore.ToString();
    }
}
