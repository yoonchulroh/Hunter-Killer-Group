using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortManager : MonoBehaviour
{
    private Dictionary<int, GameObject> _portDict = new Dictionary<int, GameObject>();
    public Dictionary<int, GameObject> portDict => _portDict;

    private Dictionary<ResourceType, List<int>> _consumerPortDict = new Dictionary<ResourceType, List<int>>();
    public Dictionary<ResourceType, List<int>> consumerPortDict => _consumerPortDict;

    private Dictionary<ResourceType, int> _producerPortDict = new Dictionary<ResourceType, int>();
    public Dictionary<ResourceType, int> producerPortDict => _producerPortDict;

    public void AddNewPort(int id, GameObject port, PortType portType, ResourceType resourceType)
    {
        _portDict.Add(id, port);
        
        if (portType == PortType.Consumer)
        {
            try
            {
                _consumerPortDict[resourceType].Add(id);
            }
            catch
            {
                _consumerPortDict.Add(resourceType, new List<int> {id});
            }
        } else if (portType == PortType.Producer)
        {
            _producerPortDict.Add(resourceType, id);
        }
    }

    public int FindNextPort(int currentPortID, int destinationPortID)
    {
        Dictionary<int, float> distanceDict = new Dictionary<int, float>();
        Dictionary<int, int> previousPortDict = new Dictionary<int, int>();
        List<int> unvisitedList = new List<int>();

        int nextPortID;

        foreach(int portID in portDict.Keys)
        {
            distanceDict.Add(portID, float.PositiveInfinity);
            previousPortDict.Add(portID, -1);
            unvisitedList.Add(portID);
        }

        distanceDict[currentPortID] = 0;
        previousPortDict[currentPortID] = -2;
        int minDistPort = currentPortID;

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
        if (previousPortDict[destinationPortID] == -1)
        {
            return -1;
        }
        else if (previousPortDict[destinationPortID] == -2)
        {
            nextPortID = currentPortID;
            return nextPortID;
        }
        else
        {
            var prevPort = previousPortDict[destinationPortID];
            var endReached = false;
            if (prevPort == currentPortID)
            {
                nextPortID = destinationPortID;
                return nextPortID;
            }
            while (!endReached)
            {
                if (previousPortDict[prevPort] != currentPortID)
                {
                    prevPort = previousPortDict[prevPort];
                }
                else
                {
                    endReached = true;
                }
            }
            nextPortID = prevPort;
            return nextPortID;
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
