using System.Collections.Generic;
using UnityEngine;

public class RadarBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _radarOriginPrefab;
    private List<GameObject> _detectedUboats = new List<GameObject>();

    private GameObject _radarOrigin;

    void Start()
    {
        var radarOriginPosition = transform.position;
        radarOriginPosition.z = -1f;
        _radarOrigin = Instantiate<GameObject>(_radarOriginPrefab, radarOriginPosition, Quaternion.identity);
        _radarOrigin.transform.SetParent(gameObject.transform);
        _radarOrigin.GetComponent<RadarOriginBehaviour>().SetParent(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Uboat")
        {
            _detectedUboats.Add(collision.gameObject);
            GameManager.Instance.detectionManager.AddUboat(collision.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Uboat")
        {
            _detectedUboats.Remove(collision.gameObject);
            GameManager.Instance.detectionManager.RemoveUboat(collision.gameObject);
        }
    }

    public void RemoveRadar()
    {
        foreach(GameObject uboat in _detectedUboats)
        {
            GameManager.Instance.detectionManager.RemoveUboat(uboat);
        }
        Destroy(_radarOrigin);
        Destroy(gameObject);
    }
}
