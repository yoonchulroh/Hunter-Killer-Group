using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEntitySpawner : MonoBehaviour
{
    [SerializeField] protected GameObject _entityPrefab;
    [SerializeField] protected GameObject _clickFieldPrefab;

    protected GameObject SpawnEntity(Vector3 initialPosition, MovingEntityData movingEntityData)
    {
        var entity = Instantiate<GameObject>(_entityPrefab, initialPosition, Quaternion.identity);

        entity.transform.SetParent(gameObject.transform);
        
        entity.GetComponent<MovingEntityBehaviour>().SetMovingEntityProperties(movingEntityData);

        var clickField = Instantiate<GameObject>(_clickFieldPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        clickField.transform.SetParent(entity.transform, false);

        return entity;
    }
}
