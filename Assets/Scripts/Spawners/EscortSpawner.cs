using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscortSpawner : MovingEntitySpawner
{
    [SerializeField] private GameObject _radarCollection;

    public void SpawnEscortOnMousePosition()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint( new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0) );
        mousePosition.z = 0f;
        SpawnEscort(GameManager.Instance.escortManager.escortCount + 1, mousePosition);
    }

    private void SpawnEscort(int id, Vector3 position)
    {
        var escort = SpawnEntity(position, MovingEntityInitialData.Escort(id));
        GameManager.Instance.escortManager.AddNewEscort(id, escort);

        _radarCollection.GetComponent<RadarSpawner>().SpawnRadarOnShip(escort, RadarInitialData.Escort());
    }
}
