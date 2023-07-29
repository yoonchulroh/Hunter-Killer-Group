using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirfieldSpawner : StationaryEntitySpawner
{
    void Start()
    {
        SpawnAirfieldRandomly(1);
    }

    private GameObject SpawnAirfieldRandomly(int id)
    {
        var airfield = SpawnEntityRandomly(id);
        airfield.GetComponent<AirfieldBehaviour>().SetTargetCoordinates(airfield.transform.position.x, airfield.transform.position.y);
        airfield.GetComponent<AirfieldBehaviour>().SetRange(30, 10);
        airfield.GetComponent<AirfieldBehaviour>().SetAttack(50, 3f);
        return airfield;
    }
}
