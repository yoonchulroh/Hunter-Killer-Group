using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortManager : MonoBehaviour
{
    private Dictionary<int, GameObject> _portDict = new Dictionary<int, GameObject>();
    public Dictionary<int, GameObject> portDict => _portDict;

    private Dictionary<ResourceType, List<int>> _consumerPortDict = new Dictionary<ResourceType, List<int>>();
    public Dictionary<ResourceType, List<int>> consumerPortDict => _consumerPortDict;

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
        }
    }
}
