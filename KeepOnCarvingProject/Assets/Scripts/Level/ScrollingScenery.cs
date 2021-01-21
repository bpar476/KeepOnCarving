using UnityEngine;

public class ScrollingScenery : MonoBehaviour
{

    [SerializeField]
    private SharedFloat skaterDistance;

    [SerializeField]
    private float parallaxFactor = 1f;

    private float location;

    private void OnEnable()
    {
        location = transform.position.x;
    }

    private void Update()
    {
        var relativeLocation = location - skaterDistance * parallaxFactor;
        transform.position = new Vector2(relativeLocation, transform.position.y);
        if (relativeLocation < -5)
        {
            Destroy(gameObject);
        }
    }

}
