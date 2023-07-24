using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class UboatBehaviour : MonoBehaviour
{
    private enum TravelMode
    {
        Random,
        TrackConvoy
    }
    [SerializeField] private GameObject _detectorForUboatBehaviour;
    private Rigidbody2D _rigidBody2D;

    private int _id;
    public int id => _id;

    private float _speed;
    private Vector3 _destination;
    private TravelMode _travelMode;

    private List<GameObject> _detectedConvoys = new List<GameObject>();

    private int _attack = 34;
    private float _attackPeriod = 0.25f;

    private void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();

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

    public void SetID(int id)
    {
        _id = id;
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }

    public void Reveal()
    {
        GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
    }

    public void Hide()
    {
        GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
    }

    private void CheckArrivedAtDestination(float allowedRange)
    {
        if (Math.Abs(transform.position.x - _destination.x) < allowedRange && Math.Abs(transform.position.y - _destination.y) < allowedRange)
        {
            if (_travelMode == TravelMode.Random)
            {
                SetDestinationRandomly();
            }
        }
    }

    public void SetDestinationRandomly()
    {
        _destination = new Vector3(Random.Range(-40, 40), Random.Range(-20, 20), 0f);
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
