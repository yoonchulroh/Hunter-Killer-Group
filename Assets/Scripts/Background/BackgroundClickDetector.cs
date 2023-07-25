using UnityEngine;

public class BackgroundClickDetector : MonoBehaviour
{
    private GameObject _radarSpawner;
    private GameObject _escortSpawner;

    void Start()
    {
        _radarSpawner = GameObject.FindWithTag("RadarSpawner");
        _escortSpawner = GameObject.FindWithTag("EscortSpawner");
    }
    void OnMouseDown()
    {
        if (GameManager.Instance.editManager.editMode == EditMode.CreateRadar)
        {
            _radarSpawner.GetComponent<RadarSpawner>().SpawnRadarOnMousePosition(true);
        }
        if (GameManager.Instance.editManager.editMode == EditMode.CreateEscort)
        {
            _escortSpawner.GetComponent<EscortSpawner>().SpawnEscortOnMousePosition();
        }
    }
}
