﻿using UnityEngine;
using TMPro;
public class SkaterProgressUI : MonoBehaviour
{

    [SerializeField]
    private EventBusContainer eventBusContainer;

    [SerializeField]
    private SharedFloat score;

    private EventBus eventBus;

    private TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
        eventBus = eventBusContainer.Bus;
    }

    private void Update()
    {
        text.text = ((int)score.Value).ToString();
    }

}
