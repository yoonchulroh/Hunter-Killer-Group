using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UboatBaseSpawner : StationaryEntitySpawner
{
    void Start()
    {
        SpawnEntityRandomly(1);
    }
}
