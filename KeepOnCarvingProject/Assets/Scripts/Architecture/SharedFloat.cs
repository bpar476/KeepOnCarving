using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "SharedFloat", menuName = "KeepOnCarving/SharedFloat", order = 50)]
public class SharedFloat : ScriptableObject
{

#if UNITY_EDITOR
    [Multiline]
    public string DeveloperDescription = "";
#endif

    [SerializeField]
    private float defaultValue;

    public float Value;

    public float DefaultValue { get { return defaultValue; } }

    private void OnEnable()
    {
        Value = defaultValue;
        // Reset value between scenes
        SceneManager.sceneLoaded += (_1, _2) => Value = defaultValue;
    }

    public static float operator *(SharedFloat a, float b)
    {
        return a.Value * b;
    }

    public static float operator /(SharedFloat a, float b)
    {
        return a.Value / b;
    }

    public static float operator +(SharedFloat a, float b)
    {
        return a.Value + b;
    }

    public static float operator -(SharedFloat a, float b)
    {
        return a.Value - b;
    }

    public static float operator -(float a, SharedFloat b)
    {
        return a - b.Value;
    }

    public static bool operator >(SharedFloat a, float b)
    {
        return a.Value > b;
    }

    public static bool operator <(SharedFloat a, float b)
    {
        return a.Value < b;
    }

    public static bool operator ==(SharedFloat a, float b)
    {
        return a.Value == b;
    }

    public static bool operator !=(SharedFloat a, float b)
    {
        return a.Value != b;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public override bool Equals(object other)
    {
        return Value.Equals(other);
    }
}
