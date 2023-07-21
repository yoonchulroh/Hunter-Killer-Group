using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeawayBehaviour : MonoBehaviour
{
    private GameObject _portManager;

    private int _end1;
    private int _end2;

    public int end1 => _end1;
    public int end2 => _end2;

    void Awake()
    {
        _portManager = GameObject.FindWithTag("PortSpawner");
    }

    public void SetEnds(int end1, int end2)
    {
        _end1 = end1;
        _end2 = end2;

        var end1coordinates = GameManager.Instance.portManager.portDict[end1].GetComponent<PortBehaviour>().coordinate;
        end1coordinates.z = 1;
        var end2coordinates = GameManager.Instance.portManager.portDict[end2].GetComponent<PortBehaviour>().coordinate;
        end2coordinates.z = 1;
        GetComponent<LineRenderer>().SetPosition(0, end1coordinates);
        GetComponent<LineRenderer>().SetPosition(1, end2coordinates);
    }
}
