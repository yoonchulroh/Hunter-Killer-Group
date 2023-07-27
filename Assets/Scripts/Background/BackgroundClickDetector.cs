using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class BackgroundClickDetector : MonoBehaviour
{
    private GameObject _radarSpawner;
    private GameObject _escortSpawner;

    void Start()
    {
        _radarSpawner = GameObject.FindWithTag("RadarCollection");
        _escortSpawner = GameObject.FindWithTag("EscortSpawner");
    }
    void OnMouseDown()
    {
        if (!IsPointerOverUIObject())
        {
            if (GameManager.Instance.editManager.editMode == EditMode.CreateRadar)
            {
                _radarSpawner.GetComponent<RadarSpawner>().SpawnRadarOnMousePosition();
            }
            if (GameManager.Instance.editManager.editMode == EditMode.CreateEscort)
            {
                _escortSpawner.GetComponent<EscortSpawner>().SpawnEscortOnMousePosition();
            }
            if (GameManager.Instance.editManager.editMode == EditMode.Select)
            {
                GameManager.Instance.editManager.SwitchEditMode(EditMode.None);
            }
        }
    }

    public void PassMouseDown()
    {
        OnMouseDown();
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
