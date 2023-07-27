using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirfieldSpawner : StationaryEntitySpawner
{
    void Start()
    {
        SpawnEntityRandomly(1);
    }
}
