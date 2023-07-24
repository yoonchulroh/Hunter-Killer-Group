using UnityEngine;

public class BackgroundClickDetector : MonoBehaviour
{
    private GameObject _radarSpawner;

    void Start()
    {
        _radarSpawner = GameObject.FindWithTag("RadarSpawner");
    }
    void OnMouseDown()
    {
        if (GameManager.Instance.createManager.createMode == CreateMode.Radar)
        {
            _radarSpawner.GetComponent<RadarSpawner>().SpawnRadarOnMousePosition();
        }
    }

    
}
