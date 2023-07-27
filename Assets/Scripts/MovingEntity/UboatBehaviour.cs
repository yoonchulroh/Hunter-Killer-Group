using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class UboatBehaviour : MovingEntityBehaviour
{
    private enum UboatTravelMode
    {
        Random,
        TrackFriendly
    }

    private UboatTravelMode _travelMode;

    private Dictionary<GameObject, int> _detectedFriendlyCountDict => GameManager.Instance.detectionManager.detectedFriendlyCountDict;

    private GameObject _detectorForUboat;

    public override void Start()
    {
        base.Start();

        _identification = "U-" + movingEntityData.id.ToString();
        _role = "Type VII";

        var identificationText = Instantiate<GameObject>(_labelPrefab, new Vector3(0, 1, 0), Quaternion.identity);
        identificationText.transform.SetParent(gameObject.transform, false);
        identificationText.GetComponent<LabelTextBehaviour>().SetIdentificationLabel(gameObject);

        var roleText = Instantiate<GameObject>(_labelPrefab, new Vector3(0, -1, 0), Quaternion.identity);
        roleText.transform.SetParent(gameObject.transform, false);
        roleText.GetComponent<LabelTextBehaviour>().SetRoleLabel(gameObject);

        _travelMode = UboatTravelMode.Random;
        SetDestinationRandomly();

        StartCoroutine(AttackClosestFriendly());
    }

    private void Update()
    {
        var currentPosition = transform.position;
        var directionalVector = _movingEntityData.speed * (_destination - currentPosition).normalized;
        _rigidBody2D.velocity = directionalVector;

        if (_travelMode == UboatTravelMode.Random)
        {
            CheckArrivedAtDestination(0.1f);
        }

        //setting new destination coordinates
        if (_detectedFriendlyCountDict.Count > 0)
        {
            _travelMode = UboatTravelMode.TrackFriendly;
            SetDestinationOnClosestFriendly();
        }
        else
        {
            if (_travelMode != UboatTravelMode.Random)
            {
                _travelMode = UboatTravelMode.Random;
                SetDestinationRandomly();
            }
        }
    }

    public void SetDetectorForUboat(GameObject detectorForUboat)
    {
        _detectorForUboat = detectorForUboat;
    }

    public void Reveal()
    {
        GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
    }

    public void Hide()
    {
        GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
    }

    public override void CheckArrivedAtDestination(float allowedRange)
    {
        if (Math.Abs(transform.position.x - _destination.x) < allowedRange && Math.Abs(transform.position.y - _destination.y) < allowedRange)
        {
            if (_travelMode == UboatTravelMode.Random)
            {
                SetDestinationRandomly();
            }
        }
    }

    public override void Destroyed()
    {
        Destroy(_detectorForUboat);
        base.Destroyed();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (_travelMode == UboatTravelMode.Random)
        {
            SetDestinationRandomly();
        }
    }

    private bool SetDestinationOnClosestFriendly()
    {
        _destination = GameManager.Instance.detectionManager.ClosestDetectedFriendly(transform.position).transform.position;

        if (_destination == null)
        {
            return false;
        } else {
            return true;
        }
    }

    private IEnumerator AttackClosestFriendly()
    {
        GameObject closestFriendly = null;
        while (true)
        {
            closestFriendly = GameManager.Instance.detectionManager.ClosestDetectedFriendly(transform.position);
            if (closestFriendly != null && Vector3.Distance(closestFriendly.transform.position, transform.position) < _movingEntityData.attackRange)
            {
                closestFriendly.GetComponent<MovingEntityBehaviour>().Attacked(_movingEntityData.attack);
            }
            yield return new WaitForSeconds(_movingEntityData.attackPeriod);
        }
    }
}
