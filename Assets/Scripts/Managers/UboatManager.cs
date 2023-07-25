using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UboatManager : MonoBehaviour
{
    private Dictionary<int, GameObject> _uboatDict = new Dictionary<int, GameObject>();
    public Dictionary<int, GameObject> uboatDict => _uboatDict;

    private int _uboatCount = 0;
    public int uboatCount => _uboatCount;

    public void AddNewUboat(int id, GameObject uboat)
    {
        _uboatDict.Add(id, uboat);
        _uboatCount += 1;
    }
}
