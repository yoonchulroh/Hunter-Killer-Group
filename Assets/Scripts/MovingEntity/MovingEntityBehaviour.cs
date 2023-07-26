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

    protected GameObject _hpText;

    protected float _speed;
    public float speed;

    protected Vector3 _destination;

    protected int _id;
    public int id => _id;

    protected int _hp;
    public int hp;

    protected int _attack = 5;
    public int attack => _attack;

    protected float _attackPeriod;
    public float attackPeriod => _attackPeriod;

    protected float _attackRange;
    public float attackRange => _attackRange;

    public virtual void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _destroyedEntityCollectionObject = GameObject.FindWithTag("DestroyedEntityCollection");

        _hpText = Instantiate<GameObject>(_labelPrefab, new Vector3(-2, 0, 0), Quaternion.identity);
        _hpText.transform.SetParent(gameObject.transform, false);
        _hpText.GetComponent<LabelTextBehaviour>().SetHpLabel(_hp);
    }

    public void SetMovingEntityProperties(int id, int hp, float speed)
    {
        _id = id;
        _speed = speed;
        _hp = hp;
    }

    public void SetMovingEntityAttackProperties(int attack, float attackPeriod, float attackRange)
    {
        _attack = attack;
        _attackPeriod = attackPeriod;
        _attackRange = attackRange;
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
        _hpText.GetComponent<LabelTextBehaviour>().SetHpLabel(_hp);
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
