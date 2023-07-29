using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class BackgroundClickDetector : MonoBehaviour, IPointerClickHandler
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
            GameManager.Instance.editManager.selectedObject = null;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (GameManager.Instance.editManager.selectedObject != null && GameManager.Instance.editManager.selectedObject.tag == "Escort")
            {
                GameManager.Instance.editManager.selectedObject.GetComponent<EscortBehaviour>().PointOrder(Camera.main.ScreenToWorldPoint( new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0) ));
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
        return results.Count > 1;
    }
}
