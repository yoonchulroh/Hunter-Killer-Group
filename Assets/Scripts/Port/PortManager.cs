using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortManager : MonoBehaviour
{
    private Dictionary<int, GameObject> _portDict = new Dictionary<int, GameObject>();
    public Dictionary<int, GameObject> portDict => _portDict;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void AddNewPort(int id, GameObject port)
    {
        _portDict.Add(id, port);
    }
}
