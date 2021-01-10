using UnityEngine;

[CreateAssetMenu(fileName = "SharedFloat", menuName = "KeepOnCarving/SharedFloat", order = 50)]
public class SharedFloat : ScriptableObject {

#if UNITY_EDITOR
    [Multiline]
    public string DeveloperDescription = "";
#endif

    [SerializeField]
    private float defaultValue;

    public float Value;

    private void OnEnable() {
        Value = defaultValue;
    }
}
