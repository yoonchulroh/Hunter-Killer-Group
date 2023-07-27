using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class UboatSpawner : MovingEntitySpawner
{
    [SerializeField] private GameObject _radarCollection;

    void Start()
    {
        for (int i = 1; i < 6; ++i)
        {
            SpawnUboat(i, Random.Range(-40, 40), Random.Range(-40, 40));
        }
    }

    private void SpawnUboat(int id, float xPos, float yPos)
    {
        var initialPosition = new Vector3(xPos, yPos, 0);

        var uboat = SpawnEntity(initialPosition, MovingEntityInitialData.Uboat(id));
        GameManager.Instance.uboatManager.AddNewEntity(id, uboat);
        _radarCollection.GetComponent<RadarSpawner>().SpawnDetectorForUboat(uboat, RadarInitialData.Uboat());
    }
}
