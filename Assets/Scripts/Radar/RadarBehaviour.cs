using System.Collections.Generic;
using UnityEngine;

public class RadarBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _radarOriginPrefab;
    private List<GameObject> _detectedUboats = new List<GameObject>();

    private bool _shipRadar = false;
    private GameObject _parent;

    private GameObject _radarOrigin;

    void Update()
    {
        if (_shipRadar)
        {
            transform.position = _parent.transform.position;
        }
    }

    public void CreateRadarOrigin()
    {
        var radarOriginPosition = transform.position;
        radarOriginPosition.z = -1f;
        _radarOrigin = Instantiate<GameObject>(_radarOriginPrefab, radarOriginPosition, Quaternion.identity);
        _radarOrigin.transform.SetParent(gameObject.transform);
        _radarOrigin.GetComponent<RadarOriginBehaviour>().SetParent(gameObject);
    }

    public void SetShipParent(GameObject parent)
    {
        _shipRadar = true;
        _parent = parent;
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
        if (_radarOrigin != null)
        {
            Destroy(_radarOrigin);
        }
        Destroy(gameObject);
    }
}
