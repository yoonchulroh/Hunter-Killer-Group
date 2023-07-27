using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SeawayBehaviour : MonoBehaviour, IPointerClickHandler
{
    private GameObject _portManager;
    private GameObject _backgroundClickDetector;

    private int _end1;
    private int _end2;

    public int end1 => _end1;
    public int end2 => _end2;

    private Vector3 _end1coordinates;
    private Vector3 _end2coordinates;

    private Vector3 _perpendicularVector;
    private float colliderHalfWidth = 1f;

    void Awake()
    {
        _portManager = GameObject.FindWithTag("PortSpawner");
        _backgroundClickDetector = GameObject.FindWithTag("BackgroundClickDetector");
    }

    void Start()
    {
        GenerateCollider();
    }

    private void GenerateCollider()
    {
        var tiltVector = _end2coordinates - _end1coordinates;
        var _perpendicularVector = new Vector3(tiltVector.y, -tiltVector.x, 0f);
        var polygonCollider = GetComponent<PolygonCollider2D>();

        var point1 = _end1coordinates + colliderHalfWidth * _perpendicularVector.normalized;
        var point2 = _end1coordinates - colliderHalfWidth * _perpendicularVector.normalized;
        var point3 = _end2coordinates - colliderHalfWidth * _perpendicularVector.normalized;
        var point4 = _end2coordinates + colliderHalfWidth * _perpendicularVector.normalized;

        polygonCollider.SetPath(0, new List<Vector2> {point1, point2, point3, point4} );
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (GameManager.Instance.editManager.editMode == EditMode.Delete)
            {
                GameManager.Instance.seawayManager.RemoveSeaway(_end1, _end2);
                Destroy(gameObject);
            }
            else {
                _backgroundClickDetector.GetComponent<BackgroundClickDetector>().PassMouseDown();
            }
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (GameManager.Instance.editManager.editMode == EditMode.Select && GameManager.Instance.editManager.selectedObject.tag == "Escort")
            {
                GameManager.Instance.editManager.selectedObject.GetComponent<EscortBehaviour>().PatrolSeawayOrder(_end1coordinates, _end2coordinates);
            }
        }
    }

    public void SetEnds(int end1, int end2)
    {
        _end1 = end1;
        _end2 = end2;

        _end1coordinates = GameManager.Instance.portManager.portDict[end1].GetComponent<PortBehaviour>().coordinate;
        _end1coordinates.z = 1;
        _end2coordinates = GameManager.Instance.portManager.portDict[end2].GetComponent<PortBehaviour>().coordinate;
        _end2coordinates.z = 1;
        GetComponent<LineRenderer>().SetPosition(0, _end1coordinates);
        GetComponent<LineRenderer>().SetPosition(1, _end2coordinates);
    }
}
