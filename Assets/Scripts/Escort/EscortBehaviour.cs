using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EscortBehaviour : MonoBehaviour
{
    private Rigidbody2D _rigidBody2D;

    private float _speed;

    private Vector3 _destination;

    void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        SetDestinationRandomly();
    }

    void Update()
    {
        var currentPosition = transform.position;
        var directionalVector = _speed * (_destination - currentPosition).normalized;
        _rigidBody2D.velocity = directionalVector;

        CheckArrivedAtDestination(0.1f);
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }

    public void SetDestinationRandomly()
    {
        _destination = new Vector3(Random.Range(-40, 40), Random.Range(-20, 20), 0f);
    }

    private void CheckArrivedAtDestination(float allowedRange)
    {
        if (Math.Abs(transform.position.x - _destination.x) < allowedRange && Math.Abs(transform.position.y - _destination.y) < allowedRange)
        {
            SetDestinationRandomly();
        }
    }
}
