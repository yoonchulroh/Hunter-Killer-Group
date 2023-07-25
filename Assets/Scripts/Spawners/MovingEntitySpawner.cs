using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEntitySpawner : MonoBehaviour
{
    [SerializeField] protected GameObject _entityPrefab;

    protected GameObject SpawnEntity(Vector3 initialPosition, int entityID, float entitySpeed, int entityHp)
    {
        var entity = Instantiate<GameObject>(_entityPrefab, initialPosition, Quaternion.identity);

        entity.transform.SetParent(gameObject.transform);

        entity.GetComponent<MovingEntityBehaviour>().SetID(entityID);
        entity.GetComponent<MovingEntityBehaviour>().SetSpeed(entitySpeed);
        entity.GetComponent<MovingEntityBehaviour>().SetHp(entityHp);

        return entity;
    }
}
