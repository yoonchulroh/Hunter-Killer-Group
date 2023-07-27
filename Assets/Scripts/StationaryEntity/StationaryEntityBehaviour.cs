using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryEntityBehaviour : MonoBehaviour
{
    protected int _id;
    public int id => _id;
    protected float _xCoordinate;
    protected float _yCoordinate;

    public virtual void SetID(int id)
    {
        _id = id;
    }

    public virtual void SetCoordinate(float xCoordinate, float yCoordinate)
    {
        _xCoordinate = xCoordinate;
        _yCoordinate = yCoordinate;
    }

    public virtual Vector3 Coordinate()
    {
        return new Vector3(_xCoordinate, _yCoordinate, 0);
    }
}
