using UnityEngine;

public class RadarOriginBehaviour : MonoBehaviour
{
    private GameObject _stationaryRadar;

    public void SetStationaryRadar(GameObject stationaryRadar)
    {
        _stationaryRadar = stationaryRadar;
    }

    void OnMouseDown()
    {
        if (GameManager.Instance.editManager.editMode == EditMode.Delete)
        {
            _stationaryRadar.GetComponent<RadarBehaviour>().RemoveRadar();
        }
    }
}
