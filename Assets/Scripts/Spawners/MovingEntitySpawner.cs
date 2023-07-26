using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEntitySpawner : MonoBehaviour
{
    [SerializeField] protected GameObject _entityPrefab;

    protected GameObject SpawnEntity(Vector3 initialPosition, int entityID, int entityHp, float entitySpeed, int attack, float attackPeriod, float attackRange)
    {
        var entity = Instantiate<GameObject>(_entityPrefab, initialPosition, Quaternion.identity);

        entity.transform.SetParent(gameObject.transform);

        entity.GetComponent<MovingEntityBehaviour>().SetMovingEntityProperties(entityID, entityHp, entitySpeed);
        entity.GetComponent<MovingEntityBehaviour>().SetMovingEntityAttackProperties(attack, attackPeriod, attackRange);

        return entity;
    }
}
