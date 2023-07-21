using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SeawayManager : MonoBehaviour
{
    private Dictionary<int, List<object[]>> _seawayDict = new Dictionary<int, List<object[]>>();
    public Dictionary<int, List<object[]>> seawayDict => _seawayDict;
    private PortManager _portManager;
    
    void Start()
    {
        _portManager = GameManager.Instance.portManager;
    }
    public bool AddSeaway(int end1, int end2)
    {
        var exists = false;
        var distance = Vector3.Distance(_portManager.portDict[end1].GetComponent<PortBehaviour>().coordinate, _portManager.portDict[end2].GetComponent<PortBehaviour>().coordinate);

        if (!CheckDestinationInList(end1, end2))
        {
            try
            {
                _seawayDict[end1].Add(new object[] {end2, distance});
            }
            catch
            {
                _seawayDict.Add(end1, new List<object[]>() { new object[] {end2, distance} } );
            }
        }
        else
        {
            exists = true;
        }

        if (!CheckDestinationInList(end2, end1))
        {
            try
            {
                _seawayDict[end2].Add(new object[] {end1, distance});
            }
            catch
            {
                _seawayDict.Add(end2, new List<object[]>() { new object[] {end1, distance} } );
            }
        }
        else
        {
            exists = true;
        }

        Debug.Log(CheckDistance(end1, end2));
        Debug.Log(CheckDistance(end1, end2));
        return exists;
    }

    private bool CheckDestinationInList(int origin, int destination)
    {
        try
        {
            var contains = false;
            foreach (object[] idDistanceArr in _seawayDict[origin])
            {
                if (Convert.ToUInt32(idDistanceArr[0]) == destination) 
                {
                    contains = true;
                }
            }
            return contains;
        }
        catch
        {
            return false;
        }
    }

    private float CheckDistance(int origin, int destination)
    {
        try
        {
            foreach (object[] idDistanceArr in _seawayDict[origin])
            {
                if (Convert.ToUInt32(idDistanceArr[0]) == destination)
                {
                    return Convert.ToSingle(idDistanceArr[1]);
                }
            }
            return float.PositiveInfinity;
        }
        catch
        {
            return float.PositiveInfinity;
        }
    }
}
