using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ConvoyBehaviour : MovingEntityBehaviour
{
    private int _currentPortID;
    private int _nextPortID;

    private int _originPortID;
    public int originPortID => _originPortID;
    private int _destinationPortID;
    public int destinationPortID => _destinationPortID;

    private bool _routeFound;

    private ResourceType _resourceType;
    public ResourceType resourceType => _resourceType;

    private float _resourceAmount = 10f;
    public float resourceAmount => _resourceAmount;

    public override void Start()
    {
        base.Start();
        _destroyedEntityCollectionObject = GameObject.FindWithTag("DestroyedConvoyCollection");

        var nameText = Instantiate<GameObject>(_labelPrefab, new Vector3(0, 1, 0), Quaternion.identity);
        nameText.transform.SetParent(gameObject.transform, false);
        nameText.GetComponent<LabelTextBehaviour>().SetNameLabel(gameObject, ParentType.Convoy);

        var roleText = Instantiate<GameObject>(_labelPrefab, new Vector3(0, -1, 0), Quaternion.identity);
        roleText.transform.SetParent(gameObject.transform, false);
        roleText.GetComponent<LabelTextBehaviour>().SetRoleLabel(gameObject, ParentType.Convoy);
    }

    private void Update()
    {
        if (_routeFound)
        {
            SetVelocity();
            CheckArrivedAtDestination(0.1f);
        }
        else
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

    public void SetRole(ResourceType resourceType)
    {
        _resourceType = resourceType;
    }

    public override void CheckArrivedAtDestination(float allowedRange)
    {
        if (Math.Abs(transform.position.x - _destination.x) < allowedRange && Math.Abs(transform.position.y - _destination.y) < allowedRange)
        {
            _currentPortID = _nextPortID;
            if (_currentPortID == _destinationPortID)
            {
                GameManager.Instance.portManager.portDict[_destinationPortID].GetComponent<PortBehaviour>().ResourceFilled(_resourceAmount);
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