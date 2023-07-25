using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class UboatSpawner : MovingEntitySpawner
{

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

        var uboat = SpawnEntity(initialPosition, id, 3f, 100);
        GameManager.Instance.uboatManager.AddNewUboat(id, uboat);
    }
}
