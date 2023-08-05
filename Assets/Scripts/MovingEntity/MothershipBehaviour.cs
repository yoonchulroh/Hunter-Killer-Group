using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothershipBehaviour : UboatBehaviour
{
    private GameObject _uboatSpawner;
    private float _uboatSpawnPeriod = 10f;

    public override void Start()
    {
        base.Start();

        _role = "Type XIV";

        _uboatSpawner = GameObject.FindWithTag("UboatSpawner");
        StartCoroutine(SpawnUboatPeriodically());
    }

    private IEnumerator SpawnUboatPeriodically()
    {
        while (true)
        {
            _uboatSpawner.GetComponent<UboatSpawner>().SpawnUboat(GameManager.Instance.uboatManager.movingEntityCount + 1, transform.position.x, transform.position.y);
            yield return new WaitForSeconds(_uboatSpawnPeriod);
        }
    }
}
