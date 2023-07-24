using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ConvoyBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _labelPrefab;
    [SerializeField] private GameObject _destroyedConvoyPrefab;
    private GameObject _destroyedConvoyCollectionObject;
    private Rigidbody2D _rigidBody2D;

    private int _hp = 100;
    public int hp => _hp;
    private bool _destroyed = false;
    public bool destroyed => _destroyed;


    private int _currentPortID;
    private int _nextPortID;
    private Vector3 _destination; // coordinates of _nextPortID

    private int _originPortID;
    public int originPortID => _originPortID;
    private int _destinationPortID;
    public int destinationPortID => _destinationPortID;

    private bool _routeFound;

    private float _speed;

    private int _id;
    public int id => _id;

    private ResourceType _resourceType;
    public ResourceType resourceType => _resourceType;

    private void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _destroyedConvoyCollectionObject = GameObject.FindWithTag("DestroyedConvoyCollection");

        var nameText = Instantiate<GameObject>(_labelPrefab, new Vector3(0, 1, 0), Quaternion.identity);
        nameText.transform.SetParent(gameObject.transform, false);
        nameText.GetComponent<LabelTextBehaviour>().SetNameLabel(gameObject, ParentType.Convoy);

        var roleText = Instantiate<GameObject>(_labelPrefab, new Vector3(0, -1, 0), Quaternion.identity);
        roleText.transform.SetParent(gameObject.transform, false);
        roleText.GetComponent<LabelTextBehaviour>().SetRoleLabel(gameObject, ParentType.Convoy);
    }

    private void Update()
    {
        if (_routeFound && !_destroyed)
        {
            SetVelocity();
            CheckArrivedAtDestination(0.1f);
        }
        else if (!_destroyed)
        {
            _routeFound = SetNextDestination();
            if (!_routeFound)
            {
                _rigidBody2D.velocity = new Vector3(0, 0, 0);
            }
        }
    }

    public void SetOrigin(GameObject port)
    {
        _originPortID = port.GetComponent<PortBehaviour>().id;
        _currentPortID = port.GetComponent<PortBehaviour>().id;
        transform.position = port.GetComponent<PortBehaviour>().coordinate;
    }

    public void SetDestination(GameObject port)
    {
        _destinationPortID = port.GetComponent<PortBehaviour>().id;
        _routeFound = SetNextDestination();
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }

    public void SetID(int id)
    {
        _id = id;
    }

    public void SetRole(ResourceType resourceType)
    {
        _resourceType = resourceType;
    }

    public void Attacked(int damage)
    {
        _hp -= damage;
        if (_hp <= 0)
        {
            Destroyed();
        }
    }

    private void Destroyed()
    {
        var destroyedConvoy = Instantiate<GameObject>(_destroyedConvoyPrefab, transform.position, Quaternion.identity);
        destroyedConvoy.transform.SetParent(_destroyedConvoyCollectionObject.transform, false);
        Destroy(gameObject);
        /*
        _destroyed = true;
        _speed = 0f;
        _rigidBody2D.velocity = new Vector3(0, 0, 0);
        GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);


        var currentPosition = transform.position;
        currentPosition.z = 1;
        transform.position = currentPosition;
        */
    }

    private void CheckArrivedAtDestination(float allowedRange)
    {
        if (Math.Abs(transform.position.x - _destination.x) < allowedRange && Math.Abs(transform.position.y - _destination.y) < allowedRange)
        {
            _currentPortID = _nextPortID;
            if (_currentPortID == _destinationPortID)
            {
                Destroy(gameObject);
            }
            else
            {
                _routeFound = SetNextDestination();
            }
        }
    }

    private void SetVelocity()
    {
        var currentPosition = transform.position;
        var directionalVector = _speed * (_destination - currentPosition).normalized;
        _rigidBody2D.velocity = directionalVector;
    }

    private bool SetNextDestination()
    {
        Dictionary<int, float> distanceDict = new Dictionary<int, float>();
        Dictionary<int, int> previousPortDict = new Dictionary<int, int>();
        List<int> unvisitedList = new List<int>();

        foreach(int portID in GameManager.Instance.portManager.portDict.Keys)
        {
            distanceDict.Add(portID, float.PositiveInfinity);
            previousPortDict.Add(portID, -1);
            unvisitedList.Add(portID);
        }

        distanceDict[_currentPortID] = 0;
        previousPortDict[_currentPortID] = -2;
        int minDistPort = _currentPortID;

        while (unvisitedList.Count > 0 && distanceDict[minDistPort] != float.PositiveInfinity)
        {
            unvisitedList.Remove(minDistPort);
            try
            {
                foreach (object[] idDistArr in GameManager.Instance.seawayManager.seawayDict[minDistPort])
                {
                    if (unvisitedList.Contains(Convert.ToInt32(idDistArr[0])))
                    {
                        if ( distanceDict[minDistPort] + Convert.ToSingle(idDistArr[1]) < distanceDict[Convert.ToInt32(idDistArr[0])] )
                        {
                            distanceDict[Convert.ToInt32(idDistArr[0])] = distanceDict[minDistPort] + Convert.ToSingle(idDistArr[1]);
                            previousPortDict[Convert.ToInt32(idDistArr[0])] = minDistPort;
                        }
                    }
                }
            }
            catch
            {

            }
            
            minDistPort = FindMinDistPort(unvisitedList, distanceDict);
        }
        if (previousPortDict[_destinationPortID] == -1)
        {
            return false;
        }
        else if (previousPortDict[_destinationPortID] == -2)
        {
            _nextPortID = _currentPortID;
            _destination = GameManager.Instance.portManager.portDict[_currentPortID].GetComponent<PortBehaviour>().coordinate;
            return true;
        }
        else
        {
            var prevPort = previousPortDict[_destinationPortID];
            var endReached = false;
            if (prevPort == _currentPortID)
            {
                _nextPortID = _destinationPortID;
                _destination = GameManager.Instance.portManager.portDict[_destinationPortID].GetComponent<PortBehaviour>().coordinate;
                return true;
            }
            while (!endReached)
            {
                if (previousPortDict[prevPort] != _currentPortID)
                {
                    prevPort = previousPortDict[prevPort];
                }
                else
                {
                    endReached = true;
                }
            }
            _nextPortID = prevPort;
            _destination = GameManager.Instance.portManager.portDict[prevPort].GetComponent<PortBehaviour>().coordinate;
            return true;
        }
    }

    private int FindMinDistPort(List<int> unvisitedList, Dictionary<int, float> distanceDict)
    {
        if (unvisitedList.Count == 0)
        {
            return -1;
        }
        else
        {
            int minDistPort = unvisitedList[0];
            float minDist = distanceDict[minDistPort];

            foreach (int portID in unvisitedList)
            {
                if (distanceDict[portID] < minDist)
                {
                    minDistPort = portID;
                    minDist = distanceDict[portID];
                }
            }

            return minDistPort;
        }
    }
}