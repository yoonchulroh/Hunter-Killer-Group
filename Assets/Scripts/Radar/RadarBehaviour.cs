using UnityEngine;

public class RadarBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _radarOriginPrefab;

    void Start()
    {
        var radarOrigin = Instantiate<GameObject>(_radarOriginPrefab, transform.position, Quaternion.identity);
        radarOrigin.transform.SetParent(gameObject.transform);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Uboat")
        {
            GameManager.Instance.detectionManager.AddUboat(collision.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Uboat")
        {
            GameManager.Instance.detectionManager.RemoveUboat(collision.gameObject);
        }
    }
}
