using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeawayBehaviour : MonoBehaviour
{
    private GameObject _portManager;

    private int _end1;
    private int _end2;

    public int end1 => _end1;
    public int end2 => _end2;

    private Vector3 _end1coordinates;
    private Vector3 _end2coordinates;

    private Vector3 _perpendicularVector;
    private float colliderHalfWidth = 0.3f;

    void Awake()
    {
        _portManager = GameObject.FindWithTag("PortSpawner");
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

    void OnMouseDown()
    {
        if (GameManager.Instance.editManager.editMode == EditMode.Delete)
        {
            GameManager.Instance.seawayManager.RemoveSeaway(_end1, _end2);
            Destroy(gameObject);
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
