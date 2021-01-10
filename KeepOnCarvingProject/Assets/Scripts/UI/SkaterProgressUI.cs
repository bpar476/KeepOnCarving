using UnityEngine;
using TMPro;
public class SkaterProgressUI : MonoBehaviour
{

    [SerializeField]
    private SharedFloat distance;

    private TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        text.text = ((int)distance.Value).ToString();
    }

}
