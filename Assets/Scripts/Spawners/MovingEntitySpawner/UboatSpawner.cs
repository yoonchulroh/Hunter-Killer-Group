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
            //SpawnUboatRandomly();
        }
    }

    public void SpawnUboat(int id, float xPos, float yPos)
    {
        var initialPosition = new Vector3(xPos, yPos, 0);

        var uboat = SpawnEntity(initialPosition, MovingEntityInitialData.Uboat(id));
        GameManager.Instance.uboatManager.AddNewEntity(id, uboat);
        _radarCollection.GetComponent<RadarSpawner>().SpawnDetectorForUboat(uboat, RadarInitialData.Uboat());
    }

    private void SpawnUboatRandomly()
    {
        var xLimit = GameManager.Instance.cameraManager.gameObjectXLimit;
        var yLimit = GameManager.Instance.cameraManager.gameObjectYLimit;
        SpawnUboat(GameManager.Instance.uboatManager.movingEntityCount + 1, Random.Range(-xLimit, xLimit), Random.Range(-yLimit, yLimit));
    }
}
