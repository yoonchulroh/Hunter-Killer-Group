using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryEntityBehaviour : MonoBehaviour
{
    protected int _id;
    public int id => _id;
    protected Vector3 _coordinate;
    public Vector3 coordinate => _coordinate;

    public virtual void SetID(int id)
    {
        _id = id;
    }

    public virtual void SetCoordinate(Vector3 coordinate)
    {
        _coordinate = coordinate;
    }
}
