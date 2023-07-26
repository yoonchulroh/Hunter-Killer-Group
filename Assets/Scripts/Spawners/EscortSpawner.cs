using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscortSpawner : MovingEntitySpawner
{
    [SerializeField] private GameObject _shipRadarPrefab;
    private float _range = 10f;

    public void SpawnEscortOnMousePosition()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint( new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0) );
        mousePosition.z = 0f;
        SpawnEscort(GameManager.Instance.escortManager.escortCount + 1, mousePosition);
    }

    private void SpawnEscort(int id, Vector3 position)
    {
        var escort = SpawnEntity(position, id, 300, 5f, 20, 1f, 2f);
        GameManager.Instance.escortManager.AddNewEscort(id, escort);

        SpawnRadarOnShip(escort);
    }

    public void SpawnRadarOnShip(GameObject ship)
    {
        var radar = Instantiate<GameObject>(_shipRadarPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        radar.transform.SetParent(ship.transform, false);
        radar.transform.localScale = new Vector3(_range, _range, _range);
        radar.GetComponent<ShipRadarBehaviour>().SetShipParent(ship);
    }
}
