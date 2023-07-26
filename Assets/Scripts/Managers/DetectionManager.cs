using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionManager : MonoBehaviour
{
    private Dictionary<GameObject, int> _detectedUboatCountDict = new Dictionary<GameObject, int>();
    public Dictionary<GameObject, int> detectedUboatCountDict => _detectedUboatCountDict;

    private Dictionary<GameObject, int> _detectedFriendlyCountDict = new Dictionary<GameObject, int>();
    public Dictionary<GameObject, int> detectedFriendlyCountDict => _detectedFriendlyCountDict;

    public void AddUboat(GameObject uboat)
    {
        try
        {
            _detectedUboatCountDict[uboat] += 1;
        }
        catch
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
            _detectedUboatCountDict.Remove(uboat);
        }
    }

    public GameObject ClosestDetectedUboat(Vector3 position)
    {
        if (_detectedUboatCountDict.Count == 0)
        {
            return null;
        }
        else
        {
            GameObject closestUboat = null;
            float closestDistance = float.PositiveInfinity;

            foreach (GameObject uboat in _detectedUboatCountDict.Keys)
            {
                if (Vector3.Distance(uboat.transform.position, position) < closestDistance)
                {
                    closestUboat = uboat;
                    closestDistance = Vector3.Distance(uboat.transform.position, position);
                }
            }

            return closestUboat;
        }
    }

    public void AddFriendly(GameObject friendly)
    {
        try
        {
            _detectedFriendlyCountDict[friendly] += 1;
        }
        catch
        {
            _detectedFriendlyCountDict.Add(friendly, 1);
        }
    }

    public void RemoveFriendly(GameObject friendly)
    {
        if (_detectedFriendlyCountDict[friendly] > 0)
        {
            _detectedFriendlyCountDict[friendly] -= 1;
        }
        if (_detectedFriendlyCountDict[friendly] == 0)
        {
            _detectedFriendlyCountDict.Remove(friendly);
        }
    }

    public GameObject ClosestDetectedFriendly(Vector3 position)
    {
        if (_detectedFriendlyCountDict.Count == 0)
        {
            return null;
        }
        else
        {
            GameObject closestFriendly = null;
            float closestDistance = float.PositiveInfinity;

            foreach (GameObject friendly in _detectedFriendlyCountDict.Keys)
            {
                if (Vector3.Distance(friendly.transform.position, position) < closestDistance)
                {
                    closestFriendly = friendly;
                    closestDistance = Vector3.Distance(friendly.transform.position, position);
                }
            }

            return closestFriendly;
        }
    }
}
