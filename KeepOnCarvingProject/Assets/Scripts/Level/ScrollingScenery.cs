using UnityEngine;

public class ScrollingScenery : MonoBehaviour
{

    [SerializeField]
    private SharedFloat skaterDistance;

    private float location;

    private void OnEnable()
    {
        location = transform.position.x;
    }

    private void Update()
    {
        var relativeLocation = location - skaterDistance;
        transform.position = new Vector2(relativeLocation, transform.position.y);
        if (relativeLocation < -5)
        {
            Destroy(gameObject);
        }
    }

}
