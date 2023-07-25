using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class UboatBehaviour : MovingEntityBehaviour
{
    private enum TravelMode
    {
        Random,
        TrackConvoy
    }
    [SerializeField] private GameObject _detectorForUboatBehaviour;

    private TravelMode _travelMode;

    private List<GameObject> _detectedConvoys = new List<GameObject>();

    private int _attack = 5;
    private float _attackPeriod = 0.25f;

    public override void Start()
    {
        base.Start();

        var nameText = Instantiate<GameObject>(_labelPrefab, new Vector3(0, 1, 0), Quaternion.identity);
        nameText.transform.SetParent(gameObject.transform, false);
        nameText.GetComponent<LabelTextBehaviour>().SetNameLabel(gameObject, ParentType.Uboat);

        var roleText = Instantiate<GameObject>(_labelPrefab, new Vector3(0, -1, 0), Quaternion.identity);
        roleText.transform.SetParent(gameObject.transform, false);
        roleText.GetComponent<LabelTextBehaviour>().SetRoleLabel(gameObject, ParentType.Uboat);

        var detectorForUboat = Instantiate<GameObject>(_detectorForUboatBehaviour, new Vector3(0, 0, 0), Quaternion.identity);
        detectorForUboat.GetComponent<DetectorForUboatBehaviour>().SetParent(gameObject);
        detectorForUboat.transform.SetParent(gameObject.transform, false);

        _travelMode = TravelMode.Random;
        SetDestinationRandomly();

        StartCoroutine(AttackClosestConvoy());
    }

    private void Update()
    {
        var currentPosition = transform.position;
        var directionalVector = _speed * (_destination - currentPosition).normalized;
        _rigidBody2D.velocity = directionalVector;

        //setting new destination coordinates
        if (_travelMode == TravelMode.Random)
        {
            CheckArrivedAtDestination(0.1f);
        }
        
        if (_detectedConvoys.Count == 0 && _travelMode == TravelMode.TrackConvoy)
        {
            _travelMode = TravelMode.Random;
            SetDestinationRandomly();
        }

        if (_detectedConvoys.Count > 0)
        {
            _travelMode = TravelMode.TrackConvoy;
            SetDestinationOnClosestConvoy();
        }
    }

    public void CollisionEnter(Collider2D collision)
    {   
        if (collision.gameObject.tag == "Convoy")
        {
            Debug.Assert(!_detectedConvoys.Contains(collision.gameObject));
            _detectedConvoys.Add(collision.gameObject);
        }
    }

    public void CollisionExit(Collider2D collision)
    {
        if (collision.gameObject.tag == "Convoy")
        {
            _detectedConvoys.Remove(collision.gameObject);
        }
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
            if (_travelMode == TravelMode.Random)
            {
                SetDestinationRandomly();
            }
        }
    }

    private void SetDestinationOnClosestConvoy()
    {
        _destination = ClosestDetectedConvoy().transform.position;
    }

    private IEnumerator AttackClosestConvoy()
    {
        while (true)
        {
            if (_detectedConvoys.Count > 0)
            {
                ClosestDetectedConvoy().GetComponent<ConvoyBehaviour>().Attacked(_attack);
            }
            yield return new WaitForSeconds(_attackPeriod);
        }
    }

    private GameObject ClosestDetectedConvoy()
    {
        if (_detectedConvoys.Count == 0)
        {
            return null;
        }
        else
        {
            var closestConvoy = _detectedConvoys[0];

            foreach (GameObject convoy in _detectedConvoys)
            {
                if (Vector3.Distance(transform.position, convoy.transform.position) < Vector3.Distance(transform.position, closestConvoy.transform.position))
                {
                    closestConvoy = convoy;
                }
            }
            return closestConvoy;
        }
    }
}
