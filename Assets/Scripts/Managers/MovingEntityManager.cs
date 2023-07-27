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

    public List<GameObject> EntitiesInRange(float xPos, float yPos, float range)
    {
        var entitiesInRange = new List<GameObject>();
        var basePosition = new Vector2(xPos, yPos);
        foreach (GameObject entity in _movingEntityDict.Values)
        {
            if (entity != null && Vector2.Distance(entity.GetComponent<MovingEntityBehaviour>().CurrentPosition(), basePosition) < range)
            {
                entitiesInRange.Add(entity);
            }
        }
        return entitiesInRange;
    }
}
