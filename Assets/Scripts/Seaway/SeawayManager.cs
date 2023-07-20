using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeawayManager : MonoBehaviour
{
    private Dictionary<int, List<int>> _seawayDict = new Dictionary<int, List<int>>();
    public Dictionary<int, List<int>> seawayDict => _seawayDict;

    public bool AddSeaway(int id, int end1, int end2)
    {
        var exists = false;

        try
        {
            if (!_seawayDict[end1].Contains(end2))
            {
                _seawayDict[end1].Add(end2);
            }
            else
            {
                exists = true;
            }
        }
        catch
        {
            _seawayDict.Add(end1, new List<int>() {end2} );
        }

        try
        {
            if (!_seawayDict[end2].Contains(end1))
            {
                _seawayDict[end2].Add(end1);
            }
            else
            {
                exists = true;
            }
        }
        catch
        {
            _seawayDict.Add(end2, new List<int>() {end1} );
        }
        return exists;
    }
}
