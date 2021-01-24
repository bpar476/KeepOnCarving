using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class WrappingCloud : MonoBehaviour
{

    private static readonly string MAT_PROP_DIRECTION = "_Direction";
    private static readonly string MAT_PROP_WRAP_LENGTH = "_WrapLength";
    private static readonly string MAT_PROP_OFFSET = "_Offset";

    [SerializeField]
    private Vector3 direction;
    [SerializeField]
    private Vector3 wrapLength;
    [SerializeField]
    private Vector3 offset;

    private void Awake()
    {
        var mat = GetComponent<SpriteRenderer>().material;
        mat.SetVector(MAT_PROP_DIRECTION, direction);
        mat.SetVector(MAT_PROP_WRAP_LENGTH, wrapLength);
        mat.SetVector(MAT_PROP_OFFSET, offset);
    }

}
