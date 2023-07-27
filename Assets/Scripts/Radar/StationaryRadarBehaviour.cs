using System.Collections.Generic;
using UnityEngine;

public class StationaryRadarBehaviour : RadarBehaviour
{
    [SerializeField] private GameObject _radarOriginPrefab;
    private GameObject _radarOrigin;

    public void CreateRadarOrigin(RadarData radarProperties)
    {      
        _radarOrigin = Instantiate<GameObject>(_radarOriginPrefab, new Vector3(0, 0, -1), Quaternion.identity);
        _radarOrigin.transform.SetParent(gameObject.transform, false);
        _radarOrigin.GetComponent<RadarOriginBehaviour>().SetParent(gameObject);
        _radarOrigin.transform.localScale = new Vector3(0.5f/radarProperties.range, 0.5f/radarProperties.range, 1);
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
