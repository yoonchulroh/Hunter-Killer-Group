using UnityEngine;

public class BackgroundClickDetector : MonoBehaviour
{
    private GameObject _stationaryRadarSpawner;
    private GameObject _escortSpawner;

    void Start()
    {
        _stationaryRadarSpawner = GameObject.FindWithTag("StationaryRadarSpawner");
        _escortSpawner = GameObject.FindWithTag("EscortSpawner");
    }
    void OnMouseDown()
    {
        if (GameManager.Instance.editManager.editMode == EditMode.CreateRadar)
        {
            _stationaryRadarSpawner.GetComponent<StationaryRadarSpawner>().SpawnRadarOnMousePosition();
        }
        if (GameManager.Instance.editManager.editMode == EditMode.CreateEscort)
        {
            _escortSpawner.GetComponent<EscortSpawner>().SpawnEscortOnMousePosition();
        }
    }
}
