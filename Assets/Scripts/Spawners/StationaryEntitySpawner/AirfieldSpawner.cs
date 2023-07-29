using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirfieldSpawner : StationaryEntitySpawner
{
    void Start()
    {
        SpawnAirfieldRandomly(1);
        SpawnAirfieldRandomly(2);
    }

    private GameObject SpawnAirfieldRandomly(int id)
    {
        var airfield = SpawnEntityRandomly(id);
        return airfield;
    }
}
