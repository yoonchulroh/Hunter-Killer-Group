using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscortManager : MonoBehaviour
{
    private Dictionary<int, GameObject> _escortDict = new Dictionary<int, GameObject>();
    public Dictionary<int, GameObject> escortDict => _escortDict;

    private int _escortCount = 0;
    public int escortCount => _escortCount;

    public void AddNewEscort(int id, GameObject escort)
    {
        _escortDict.Add(id, escort);
        _escortCount += 1;
    }
}
