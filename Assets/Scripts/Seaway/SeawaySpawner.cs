using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SeawaySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _portManager;
    [SerializeField] private GameObject _seawayPrefab;
    private SeawayManager _seawayManager;

    private int _seawayCount = 0;

    void Start()
    {
        _seawayManager = GetComponent<SeawayManager>();
    }

    void Update()
    {
        if (_seawayCount < 10)
        {
            var portCount = _portManager.GetComponent<PortManager>().portDict.Count;
            if (SpawnSeaway(_seawayCount, Random.Range(0, portCount), Random.Range(0, portCount)))
            {
                _seawayCount += 1;
            }
        }
    }

    private bool SpawnSeaway(int id, int end1, int end2)
    {
        if (!_seawayManager.AddSeaway(id, end1, end2))
        {
            var seaway = Instantiate<GameObject>(_seawayPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            seaway.transform.SetParent(gameObject.transform);
            seaway.GetComponent<SeawayBehaviour>().SetEnds(end1, end2);
            seaway.GetComponent<SeawayBehaviour>().SetID(id);
            return true;
        }
        else
        {
            return false;
        }
    }
}
