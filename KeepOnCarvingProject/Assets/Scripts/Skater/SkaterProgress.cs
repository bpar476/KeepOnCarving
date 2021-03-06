﻿using UnityEngine;

public class SkaterProgress : MonoBehaviour
{

    [SerializeField]
    private SharedFloat distance;

    [SerializeField]
    private SharedFloat speed;

    private void Awake()
    {
        distance.Value = 0;
    }

    private void FixedUpdate()
    {
        distance.Value += speed * Time.fixedDeltaTime;
    }

}
