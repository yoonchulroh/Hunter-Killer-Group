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

        _identification = "DD-" + movingEntityData.id.ToString();
        _role = "Clemson-class";

        var identificationText = Instantiate<GameObject>(_labelPrefab, new Vector3(0, 1, 0), Quaternion.identity);
        identificationText.transform.SetParent(gameObject.transform, false);
        identificationText.GetComponent<LabelTextBehaviour>().SetIdentificationLabel(gameObject);

        var roleText = Instantiate<GameObject>(_labelPrefab, new Vector3(0, -1, 0), Quaternion.identity);
        roleText.transform.SetParent(gameObject.transform, false);
        roleText.GetComponent<LabelTextBehaviour>().SetRoleLabel(gameObject);

        _travelMode = EscortTravelMode.Random;
        SetDestinationRandomly();

        StartCoroutine(AttackClosestUboat());
    }

    void Update()
    {
        var currentPosition = transform.position;
        var directionalVector = _movingEntityData.speed * (_destination - currentPosition).normalized;
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (_travelMode == EscortTravelMode.Random)
        {
            SetDestinationRandomly();
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
            if (closestUboat != null && Vector3.Distance(closestUboat.transform.position, transform.position) < _movingEntityData.attackRange)
            {
                closestUboat.GetComponent<MovingEntityBehaviour>().Attacked(_movingEntityData.attack);
            }
            yield return new WaitForSeconds(_movingEntityData.attackPeriod);
        }
    }
}
