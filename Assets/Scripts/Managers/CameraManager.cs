using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] GameObject _mainCamera;

    private Vector3 _cameraPosition;
    public Vector3 cameraPosition => _cameraPosition;

    private float _cameraSize;
    public float cameraSize => _cameraSize;

    private float _gameObjectYLimit;
    public float gameObjectYLimit => _gameObjectYLimit;

    private float _gameObjectXLimit;
    public float gameObjectXLimit => _gameObjectXLimit;

    public void MoveCameraToCoordinates(float xPos, float yPos)
    {
        _cameraPosition = new Vector3(xPos, yPos, -10);
        _mainCamera.transform.position = _cameraPosition;
    }

    public void SetCameraSize(float size)
    {
        _cameraSize = size;
        _mainCamera.GetComponent<Camera>().orthographicSize = size;
    }

    public void SetGameObjectLimits(float xLimit, float yLimit)
    {
        _gameObjectYLimit = yLimit;
        _gameObjectXLimit = xLimit;
    }

    void Awake()
    {
        MoveCameraToCoordinates(0, 0);
        SetCameraSize(30);
        SetGameObjectLimits(50f, 20f);
    }
}
