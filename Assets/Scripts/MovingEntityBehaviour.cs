using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MovingEntityBehaviour : MonoBehaviour
{
    [SerializeField] protected GameObject _labelPrefab;
    [SerializeField] protected GameObject _destroyedEntityPrefab;

    protected Rigidbody2D _rigidBody2D;

    protected GameObject _destroyedEntityCollectionObject;

    protected float _speed;
    public float speed;

    protected Vector3 _destination;

    protected int _hp;
    public int hp;

    protected int _id;
    public int id => _id;

    public virtual void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }

    public void SetID(int id)
    {
        _id = id;
    }

    public void SetDestinationRandomly()
    {
        _destination = new Vector3(Random.Range(-40, 40), Random.Range(-20, 20), 0f);
    }

    public virtual void CheckArrivedAtDestination(float allowedRange)
    {
        if (Math.Abs(transform.position.x - _destination.x) < allowedRange && Math.Abs(transform.position.y - _destination.y) < allowedRange)
        {
            SetDestinationRandomly();
        }
    }

    public void Attacked(int damage)
    {
        _hp -= damage;
        if (_hp <= 0)
        {
            Destroyed();
        }
    }

    void Destroyed()
    {
        var destroyedEntity = Instantiate<GameObject>(_destroyedEntityPrefab, transform.position, Quaternion.identity);
        destroyedEntity.transform.SetParent(_destroyedEntityCollectionObject.transform, false);
        Destroy(gameObject);
    }
}
