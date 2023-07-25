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

        var nameText = Instantiate<GameObject>(_labelPrefab, new Vector3(0, 1, 0), Quaternion.identity);
        nameText.transform.SetParent(gameObject.transform, false);
        nameText.GetComponent<LabelTextBehaviour>().SetNameLabel(gameObject, ParentType.Escort);

        var roleText = Instantiate<GameObject>(_labelPrefab, new Vector3(0, -1, 0), Quaternion.identity);
        roleText.transform.SetParent(gameObject.transform, false);
        roleText.GetComponent<LabelTextBehaviour>().SetRoleLabel(gameObject, ParentType.Escort);

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
