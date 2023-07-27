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
        FollowFriendly,
        PatrolSeaway,
        FollowPointOrder
    }

    private EscortTravelMode _travelMode;
    private Dictionary<GameObject, int> _detectedUboatCountDict => GameManager.Instance.detectionManager.detectedUboatCountDict;

    private GameObject _shipRadar;

    private Vector3 _nextDestination;
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

        _travelMode = EscortTravelMode.Random;
        SetDestinationRandomly();

        StartCoroutine(AttackClosestUboat());
    }

    void Update()
    {
        switch (_travelMode)
        {
            case EscortTravelMode.TrackUboat:
                if (_detectedUboatCountDict.Count == 0)
                {
                    _travelMode = EscortTravelMode.Random;
                    SetDestinationRandomly();
                } else {
                    SetDestinationOnClosestUboat();
                }
                break;
            case EscortTravelMode.FollowFriendly:
                if (_targetObject == null)
                {
                    _travelMode = EscortTravelMode.Random;
                    SetDestinationRandomly();
                } else {
                    _destination = _targetObject.transform.position;
                }
                break;
            default:
                CheckArrivedAtDestination(0.5f);
                if (_detectedUboatCountDict.Count > 0)
                {
                    _travelMode = EscortTravelMode.TrackUboat;
                    SetDestinationOnClosestUboat();
                }
                break;
        }

        var currentPosition = transform.position;
        var directionalVector = _movingEntityData.speed * (_destination - currentPosition).normalized;
        _rigidBody2D.velocity = directionalVector;
    }

    public void PatrolSeawayOrder(Vector3 end1Coordinates, Vector3 end2Coordinates)
    {
        _travelMode = EscortTravelMode.PatrolSeaway;
        if (Vector3.Distance(transform.position, end1Coordinates) < Vector3.Distance(transform.position, end2Coordinates))
        {
            _destination = end1Coordinates;
            _nextDestination = end2Coordinates;
        } else {
            _destination = end2Coordinates;
            _nextDestination = end1Coordinates;
        }
    }

    public void PointOrder(Vector3 coordinates)
    {
        _travelMode = EscortTravelMode.FollowPointOrder;
        _destination = coordinates;
    }

    public void FollowFriendlyOrder(GameObject friendly)
    {
        _travelMode = EscortTravelMode.FollowFriendly;
        _targetObject = friendly;
    }

    public override void CheckArrivedAtDestination(float allowedRange)
    {
        if (Math.Abs(transform.position.x - _destination.x) < allowedRange && Math.Abs(transform.position.y - _destination.y) < allowedRange)
        {
            if (_travelMode == EscortTravelMode.Random || _travelMode == EscortTravelMode.FollowPointOrder)
            {
                SetDestinationRandomly();
            } else if (_travelMode == EscortTravelMode.PatrolSeaway)
            {
                var tempNextDestination = _nextDestination;
                _nextDestination = _destination;
                _destination = tempNextDestination;
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
