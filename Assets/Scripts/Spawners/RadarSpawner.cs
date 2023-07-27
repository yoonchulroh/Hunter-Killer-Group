using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _stationaryRadarPrefab;
    [SerializeField] private GameObject _shipRadarPrefab;
    [SerializeField] private GameObject _detectorForUboatPrefab;

    public void SpawnRadarOnMousePosition()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint( new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0) );
        mousePosition.z = 0f;
        SpawnRadar(mousePosition, RadarInitialData.Stationary());
    }

    private void SpawnRadar(Vector3 radarPosition, RadarData radarProperties)
    {
        var radar = Instantiate<GameObject>(_stationaryRadarPrefab, radarPosition, Quaternion.identity);
        radar.transform.SetParent(gameObject.transform, false);

        radar.GetComponent<RadarBehaviour>().SetRadarProperties(radarProperties);

        radar.GetComponent<StationaryRadarBehaviour>().CreateRadarOrigin(radarProperties);
    }

    public void SpawnRadarOnShip(GameObject ship, RadarData radarProperties)
    {
        var radar = Instantiate<GameObject>(_shipRadarPrefab, new Vector3(0, 0, 1), Quaternion.identity);
        radar.transform.SetParent(gameObject.transform, false);

        radar.GetComponent<RadarBehaviour>().SetRadarProperties(radarProperties);
        
        radar.GetComponent<ShipRadarBehaviour>().SetShipParent(ship);
        ship.GetComponent<EscortBehaviour>().SetShipRadar(radar);
    }

    public void SpawnDetectorForUboat(GameObject uboat, RadarData radarProperties)
    {
        var detectorForUboat = Instantiate<GameObject>(_detectorForUboatPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        detectorForUboat.transform.SetParent(gameObject.transform, false);

        detectorForUboat.GetComponent<DetectorForUboatBehaviour>().SetRadarProperties(radarProperties);

        detectorForUboat.GetComponent<DetectorForUboatBehaviour>().SetUboatParent(uboat);
        uboat.GetComponent<UboatBehaviour>().SetDetectorForUboat(detectorForUboat);
    }
}
