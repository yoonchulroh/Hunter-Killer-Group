using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryEntitySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _entityPrefab;

    protected GameObject SpawnEntity(int id, Vector3 position)
    {
        var entity = Instantiate<GameObject>(_entityPrefab, position, Quaternion.identity);

        entity.transform.SetParent(gameObject.transform);
        
        entity.GetComponent<StationaryEntityBehaviour>().SetID(id);
        entity.GetComponent<StationaryEntityBehaviour>().SetCoordinate(position.x, position.y);

        return entity;
    }
}
