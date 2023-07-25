using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EscortBehaviour : MovingEntityBehaviour
{
    public override void Start()
    {
        base.Start();
        SetDestinationRandomly();
    }

    void Update()
    {
        var currentPosition = transform.position;
        var directionalVector = _speed * (_destination - currentPosition).normalized;
        _rigidBody2D.velocity = directionalVector;

        CheckArrivedAtDestination(0.1f);
    }
}
