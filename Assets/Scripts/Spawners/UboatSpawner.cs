using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class UboatSpawner : MovingEntitySpawner
{
    [SerializeField] private GameObject _detectorForUboatPrefab;
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
        GameManager.Instance.uboatManager.AddNewUboat(id, uboat);
        SpawnDetectorForUboat(uboat);
    }

    private void SpawnDetectorForUboat(GameObject uboat)
    {
        var detectorForUboat = Instantiate<GameObject>(_detectorForUboatPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        detectorForUboat.GetComponent<DetectorForUboatBehaviour>().SetParent(gameObject);
        detectorForUboat.transform.SetParent(_radarCollection.transform, false);
        uboat.GetComponent<UboatBehaviour>().SetDetectorForUboat(detectorForUboat);
    }
}
