using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _radarPrefab;
    private float _range = 10f;

    public void SpawnRadarOnMousePosition(bool radarOrigin)
    {
        var mousePosition = Camera.main.ScreenToWorldPoint( new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0) );
        mousePosition.z = 0f;
        SpawnRadar(mousePosition, radarOrigin);
    }

    private void SpawnRadar(Vector3 radarPosition, bool radarOrigin)
    {
        var radar = Instantiate<GameObject>(_radarPrefab, radarPosition, Quaternion.identity);
        radar.transform.SetParent(gameObject.transform, false);
        radar.transform.localScale = new Vector3(_range, _range, _range);
        if (radarOrigin)
        {
            radar.GetComponent<RadarBehaviour>().CreateRadarOrigin();
        }
    }
}
