using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEntitySpawner : MonoBehaviour
{
    [SerializeField] protected GameObject _entityPrefab;

    protected GameObject SpawnEntity(Vector3 initialPosition, MovingEntityData movingEntityData)
    {
        var entity = Instantiate<GameObject>(_entityPrefab, initialPosition, Quaternion.identity);

        entity.transform.SetParent(gameObject.transform);
        
        entity.GetComponent<MovingEntityBehaviour>().SetMovingEntityProperties(movingEntityData);

        return entity;
    }
}
