using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _radarPrefab;
    [SerializeField] private GameObject _stationaryRadarOriginPrefab;
    [SerializeField] private GameObject _radarOriginCollection;

    public void SpawnRadarOnMousePosition()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint( new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0) );
        mousePosition.z = 0f;
        SpawnStationaryRadar(mousePosition, RadarInitialData.Stationary());
    }

    private void SpawnStationaryRadar(Vector3 radarPosition, RadarData radarProperties)
    {
        var radarOrigin = Instantiate<GameObject>(_stationaryRadarOriginPrefab, radarPosition, Quaternion.identity);
        radarOrigin.transform.SetParent(_radarOriginCollection.transform, false);

        var radar = Instantiate<GameObject>(_radarPrefab, new Vector3(0, 0, 1), Quaternion.identity);
        radar.transform.SetParent(gameObject.transform, false);

        radar.GetComponent<RadarBehaviour>().SetRadarProperties(radarProperties);

        radar.GetComponent<RadarBehaviour>().SetRadarParent(radarOrigin);
        radarOrigin.GetComponent<RadarOriginBehaviour>().SetStationaryRadar(radar);
    }

    public void SpawnRadarOnShip(GameObject ship, RadarData radarProperties)
    {
        var radar = Instantiate<GameObject>(_radarPrefab, new Vector3(0, 0, 1), Quaternion.identity);
        radar.transform.SetParent(gameObject.transform, false);

        radar.GetComponent<RadarBehaviour>().SetRadarProperties(radarProperties);
        
        radar.GetComponent<RadarBehaviour>().SetRadarParent(ship);
        ship.GetComponent<EscortBehaviour>().SetShipRadar(radar);
    }

    public void SpawnDetectorForUboat(GameObject uboat, RadarData radarProperties)
    {
        var detectorForUboat = Instantiate<GameObject>(_radarPrefab, new Vector3(0, 0, 1), Quaternion.identity);
        detectorForUboat.transform.SetParent(gameObject.transform, false);

        detectorForUboat.GetComponent<RadarBehaviour>().SetRadarProperties(radarProperties);

        detectorForUboat.GetComponent<RadarBehaviour>().SetRadarParent(uboat);
        uboat.GetComponent<UboatBehaviour>().SetDetectorForUboat(detectorForUboat);
    }
}
