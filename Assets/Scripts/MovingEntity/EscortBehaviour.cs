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
        TrackUboat,
        PatrolSeaway,
        FollowPointOrder
    }

    private EscortTravelMode _travelMode;
    private EscortTravelMode _previousTravelMode;
    private Dictionary<GameObject, int> _detectedUboatCountDict => GameManager.Instance.detectionManager.detectedUboatCountDict;

    private GameObject _shipRadar;

    private Vector3 _seaway1Coordinates;
    private Vector3 _seaway2Coordinates;
    private GameObject _targetObject;

    public override void Start()
    {
        base.Start();

        _identification = "DD-" + movingEntityData.id.ToString();
        _role = "Clemson-class";

        /*
        var identificationText = Instantiate<GameObject>(_labelPrefab, new Vector3(0, 1, 0), Quaternion.identity);
        identificationText.transform.SetParent(gameObject.transform, false);
        identificationText.GetComponent<LabelTextBehaviour>().SetIdentificationLabel(gameObject);

        var roleText = Instantiate<GameObject>(_labelPrefab, new Vector3(0, -1, 0), Quaternion.identity);
        roleText.transform.SetParent(gameObject.transform, false);
        roleText.GetComponent<LabelTextBehaviour>().SetRoleLabel(gameObject);
        */

        _previousTravelMode = EscortTravelMode.Random;
        _travelMode = EscortTravelMode.Random;
        SwitchTravelMode(EscortTravelMode.Random);

        StartCoroutine(AttackClosestUboat());
    }

    void Update()
    {
        switch (_travelMode)
        {
            case EscortTravelMode.TrackUboat:
                if (_detectedUboatCountDict.Count == 0)
                {
                    SwitchTravelMode(_previousTravelMode);
                } else {
                    SetDestinationOnClosestUboat();
                }
                break;
            default:
                CheckArrivedAtDestination(0.5f);
                if (_detectedUboatCountDict.Count > 0)
                {
                    SwitchTravelMode(EscortTravelMode.TrackUboat);
                }
                break;
        }

        var currentPosition = transform.position;
        var directionalVector = _movingEntityData.speed * (_destination - currentPosition).normalized;
        _rigidBody2D.velocity = directionalVector;
    }

    private void SwitchTravelMode(EscortTravelMode newTravelMode)
    {
        if (_travelMode != newTravelMode)
        {
            _previousTravelMode = _travelMode;
        }
        _travelMode = newTravelMode;
        switch(newTravelMode)
        {
            case EscortTravelMode.Random:
                SetDestinationRandomly();
                break;
            case EscortTravelMode.TrackUboat:
                SetDestinationOnClosestUboat();
                break;
            case EscortTravelMode.PatrolSeaway:
                SetDestinationOnNextSeawayEnd();
                break;
            case EscortTravelMode.FollowPointOrder:
                break;
            default:
                break;
        }
    }

    public void PatrolSeawayOrder(Vector3 end1Coordinates, Vector3 end2Coordinates)
    {
        _seaway1Coordinates = end1Coordinates;
        _seaway2Coordinates = end2Coordinates;
        if (Vector3.Distance(transform.position, end1Coordinates) < Vector3.Distance(transform.position, end2Coordinates))
        {
            _destination = end1Coordinates;
        } else {
            _destination = end2Coordinates;
        }
        SwitchTravelMode(EscortTravelMode.PatrolSeaway);
    }

    public void PointOrder(Vector3 coordinates)
    {
        _destination = coordinates;
        SwitchTravelMode(EscortTravelMode.FollowPointOrder);
    }

    public override void CheckArrivedAtDestination(float allowedRange)
    {
        if (Math.Abs(transform.position.x - _destination.x) < allowedRange && Math.Abs(transform.position.y - _destination.y) < allowedRange)
        {
            if (_travelMode == EscortTravelMode.Random || _travelMode == EscortTravelMode.FollowPointOrder)
            {
                SwitchTravelMode(_previousTravelMode);
            } else if (_travelMode == EscortTravelMode.PatrolSeaway)
            {
                SetDestinationOnNextSeawayEnd();
            }
        }
    }

    public void SetShipRadar(GameObject shipRadar)
    {
        _shipRadar = shipRadar;
    }

    public override void Destroyed()
    {
        Destroy(_shipRadar);
        base.Destroyed();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (_travelMode == EscortTravelMode.Random)
        {
            SetDestinationRandomly();
        }
    }

    private void SetDestinationOnNextSeawayEnd()
    {
        if (Vector3.Distance(_seaway1Coordinates, transform.position) > Vector3.Distance(_seaway2Coordinates, transform.position))
        {
            _destination = _seaway1Coordinates;
        } else {
            _destination = _seaway2Coordinates;
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
