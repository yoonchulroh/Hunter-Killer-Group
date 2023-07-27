using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UboatBaseBehaviour : StationaryEntityBehaviour
{
    private GameObject _uboatSpawner;
    private float _uboatSpawnPeriod = 5f;

    void Start()
    {
        _uboatSpawner = GameObject.FindWithTag("UboatSpawner");
        StartCoroutine(SpawnUboatPeriodically());
    }

    private IEnumerator SpawnUboatPeriodically()
    {
        while (true)
        {
            _uboatSpawner.GetComponent<UboatSpawner>().SpawnUboat(GameManager.Instance.uboatManager.movingEntityCount + 1, _xCoordinate, _yCoordinate);
            yield return new WaitForSeconds(_uboatSpawnPeriod);
        }
    }
}
