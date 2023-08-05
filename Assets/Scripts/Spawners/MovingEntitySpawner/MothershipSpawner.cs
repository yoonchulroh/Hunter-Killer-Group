using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothershipSpawner : UboatSpawner
{
    private float _mothershipSpawnPeriod = 20f;
    void Start()
    {
        StartCoroutine(SpawnMothershipPeriodically());
    }

    private IEnumerator SpawnMothershipPeriodically()
    {
        while (true)
        {
            SpawnUboatRandomly();
            yield return new WaitForSeconds(_mothershipSpawnPeriod);
        }
    }
}
