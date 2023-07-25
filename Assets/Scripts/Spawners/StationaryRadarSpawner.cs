using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryRadarSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _stationaryRadarPrefab;
    private float _range = 10f;

    public void SpawnRadarOnMousePosition()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint( new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0) );
        mousePosition.z = 0f;
        SpawnRadar(mousePosition);
    }

    private void SpawnRadar(Vector3 radarPosition)
    {
        var radar = Instantiate<GameObject>(_stationaryRadarPrefab, radarPosition, Quaternion.identity);
        radar.transform.SetParent(gameObject.transform, false);
        radar.transform.localScale = new Vector3(_range, _range, _range);
        radar.GetComponent<StationaryRadarBehaviour>().CreateRadarOrigin();
    }
}
