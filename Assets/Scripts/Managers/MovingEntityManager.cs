using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEntityManager : MonoBehaviour
{
    protected Dictionary<int, GameObject> _movingEntityDict = new Dictionary<int, GameObject>();
    public Dictionary<int, GameObject> movingEntityDict => _movingEntityDict;

    public int movingEntityCount = 0;
    //public int movingEntityCount => _movingEntityCount;

    public void AddNewEntity(int id, GameObject entity)
    {
        _movingEntityDict.Add(id, entity);
        movingEntityCount += 1;
    }
}
