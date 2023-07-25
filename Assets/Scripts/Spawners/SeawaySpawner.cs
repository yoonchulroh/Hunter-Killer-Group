using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SeawaySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _portManager;
    [SerializeField] private GameObject _seawayPrefab;
    private SeawayManager _seawayManager;


    void Start()
    {
        _seawayManager = GameManager.Instance.seawayManager;
    }

    void Update()
    {

    }

    public bool SpawnSeaway(int end1, int end2)
    {
        if (!_seawayManager.AddSeaway(end1, end2))
        {
            var seaway = Instantiate<GameObject>(_seawayPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            seaway.transform.SetParent(gameObject.transform);
            seaway.GetComponent<SeawayBehaviour>().SetEnds(end1, end2);
            return true;
        }
        else
        {
            return false;
        }
    }
}
