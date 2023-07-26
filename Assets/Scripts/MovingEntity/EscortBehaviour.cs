using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EscortBehaviour : MovingEntityBehaviour
{
    private enum EscortTravelMode
    {
        Random,
        TrackUboat
    }

    private EscortTravelMode _travelMode;
    private Dictionary<GameObject, int> _detectedUboatCountDict => GameManager.Instance.detectionManager.detectedUboatCountDict;


    public override void Start()
    {
        base.Start();

        var nameText = Instantiate<GameObject>(_labelPrefab, new Vector3(0, 1, 0), Quaternion.identity);
        nameText.transform.SetParent(gameObject.transform, false);
        nameText.GetComponent<LabelTextBehaviour>().SetNameLabel(gameObject, ParentType.Escort);

        var roleText = Instantiate<GameObject>(_labelPrefab, new Vector3(0, -1, 0), Quaternion.identity);
        roleText.transform.SetParent(gameObject.transform, false);
        roleText.GetComponent<LabelTextBehaviour>().SetRoleLabel(gameObject, ParentType.Escort);

        _travelMode = EscortTravelMode.Random;
        SetDestinationRandomly();

        StartCoroutine(AttackClosestUboat());
    }

    void Update()
    {
        var currentPosition = transform.position;
        var directionalVector = _speed * (_destination - currentPosition).normalized;
        _rigidBody2D.velocity = directionalVector;

        if (_travelMode == EscortTravelMode.Random)
        {
            CheckArrivedAtDestination(0.1f);
        }

        //setting new destination coordinates
        if (_detectedUboatCountDict.Count > 0)
        {
            _travelMode = EscortTravelMode.TrackUboat;
            SetDestinationOnClosestUboat();
        }
        else
        {
            if (_travelMode != EscortTravelMode.Random)
            {
                _travelMode = EscortTravelMode.Random;
                SetDestinationRandomly();
            }
        }
    }

    private bool SetDestinationOnClosestUboat()
    {
        _destination = GameManager.Instance.detectionManager.ClosestDetectedUboat(transform.position).transform.position;

        if (_destination == null)
        {
            return false;
        } else {
            return true;
        }
    }

    private IEnumerator AttackClosestUboat()
    {
        GameObject closestUboat = null;
        while (true)
        {
            closestUboat = GameManager.Instance.detectionManager.ClosestDetectedUboat(transform.position);
            if (closestUboat != null && Vector3.Distance(closestUboat.transform.position, transform.position) < _attackRange)
            {
                closestUboat.GetComponent<MovingEntityBehaviour>().Attacked(_attack);
            }
            yield return new WaitForSeconds(_attackPeriod);
        }
    }
}
