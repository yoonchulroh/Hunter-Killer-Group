using UnityEngine;

public class DetectorForUboatBehaviour : MonoBehaviour
{
    private GameObject _parent;

    protected RadarData _radarProperties;

    private GameObject _backgroundClickDetector;
    
    void Start()
    {
        _backgroundClickDetector = GameObject.FindWithTag("BackgroundClickDetector");
        gameObject.transform.localScale = new Vector3(_radarProperties.range, _radarProperties.range, _radarProperties.range);
    }

    void Update()
    {
        transform.position = _parent.transform.position;
    }

    public void SetRadarProperties(RadarData radarProperties)
    {
        _radarProperties = radarProperties;
    }

    public void SetUboatParent(GameObject parent)
    {
        _parent = parent;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Convoy" || collision.gameObject.tag == "Escort")
        {
            GameManager.Instance.detectionManager.AddFriendly(collision.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Convoy" || collision.gameObject.tag == "Escort")
        {
            GameManager.Instance.detectionManager.RemoveFriendly(collision.gameObject);
        }
    }

    void OnMouseDown()
    {
        _backgroundClickDetector.GetComponent<BackgroundClickDetector>().PassMouseDown();
    }
}
