using System.Collections.Generic;
using UnityEngine;

public class StationaryRadarBehaviour : RadarBehaviour
{
    [SerializeField] private GameObject _radarOriginPrefab;
    private GameObject _radarOrigin;

    public void CreateRadarOrigin()
    {      
        var radarOriginPosition = transform.position;
        radarOriginPosition.z = -1f;
        _radarOrigin = Instantiate<GameObject>(_radarOriginPrefab, radarOriginPosition, Quaternion.identity);
        _radarOrigin.transform.SetParent(gameObject.transform);
        _radarOrigin.GetComponent<RadarOriginBehaviour>().SetParent(gameObject);
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
