using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscortSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _escortPrefab;
    [SerializeField] private GameObject _shipRadarPrefab;
    private float _range = 10f;

    void Start()
    {
    }

    public void SpawnEscortOnMousePosition()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint( new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0) );
        mousePosition.z = 0f;
        SpawnEscort(mousePosition);
    }

    private void SpawnEscort(Vector3 position)
    {
        var escort = Instantiate<GameObject>(_escortPrefab, position, Quaternion.identity);
        escort.transform.SetParent(gameObject.transform);
        escort.GetComponent<EscortBehaviour>().SetSpeed(5f);
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
