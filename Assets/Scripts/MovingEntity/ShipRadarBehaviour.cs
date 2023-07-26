using System.Collections.Generic;
using UnityEngine;

public class ShipRadarBehaviour : RadarBehaviour
{
    private GameObject _parent;

    public void SetShipParent(GameObject parent)
    {
        _parent = parent;
    }

    public void RemoveRadar()
    {
        foreach(GameObject uboat in _detectedUboats)
        {
            GameManager.Instance.detectionManager.RemoveUboat(uboat);
        }
        Destroy(gameObject);
    }
}
