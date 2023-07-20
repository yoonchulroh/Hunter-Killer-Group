using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortBehaviour : MonoBehaviour
{
    public Vector3 coordinate;

    private int _id;
    public int id => _id;

    void Start()
    {
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
}
