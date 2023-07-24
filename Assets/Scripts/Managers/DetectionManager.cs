using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionManager : MonoBehaviour
{
    private Dictionary<GameObject, int> _detectedUboatCountDict = new Dictionary<GameObject, int>();
    public Dictionary<GameObject, int> detectedUboatCountDict => _detectedUboatCountDict;

    public void AddUboat(GameObject uboat)
    {
        try
        {
            _detectedUboatCountDict[uboat] += 1;
        } catch
        {
            _detectedUboatCountDict.Add(uboat, 1);
        }

        if (_detectedUboatCountDict[uboat] == 1)
        {
            uboat.GetComponent<UboatBehaviour>().Reveal();
        }
    }

    public void RemoveUboat(GameObject uboat)
    {
        if (_detectedUboatCountDict[uboat] > 0)
        {
            _detectedUboatCountDict[uboat] -= 1;
        }
        if (_detectedUboatCountDict[uboat] == 0)
        {
            uboat.GetComponent<UboatBehaviour>().Hide();
        }
    }
}
