using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortBehaviour : MonoBehaviour
{
    private GameObject _convoySpawner;
    public Vector3 coordinate;

    private int _id;
    public int id => _id;

    void Awake()
    {
        _convoySpawner = GameObject.FindWithTag("ConvoyManager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCoordinate(Vector3 portCoordinate)
    {
        transform.position = portCoordinate;
        coordinate = portCoordinate;
    }

    public void SetID(int id)
    {
        _id = id;
    }

    void OnMouseDown()
    {
        _convoySpawner.GetComponent<ConvoySpawner>().SpawnConvoyOnPort(_id);
    }
}
