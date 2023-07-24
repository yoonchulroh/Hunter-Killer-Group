using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PortSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _portPrefab;
    private PortManager _portManager;

    void Start()
    {
        _portManager = GameManager.Instance.portManager;
        SpawnPort(0, Random.Range(-40, 40), Random.Range(-20, 20), PortType.Producer, ResourceType.Square);
        SpawnPort(1, Random.Range(-40, 40), Random.Range(-20, 20), PortType.Consumer, ResourceType.Square);
        SpawnPort(2, Random.Range(-40, 40), Random.Range(-20, 20), PortType.Consumer, ResourceType.Square);
        SpawnPort(3, Random.Range(-40, 40), Random.Range(-20, 20), PortType.Producer, ResourceType.Star);
        SpawnPort(4, Random.Range(-40, 40), Random.Range(-20, 20), PortType.Consumer, ResourceType.Star);
        SpawnPort(5, Random.Range(-40, 40), Random.Range(-20, 20), PortType.Consumer, ResourceType.Star);
    }

    void SpawnPort(int ID, float xPos, float yPos, PortType portType, ResourceType resourceType)
    {
        var port = Instantiate<GameObject>(_portPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        port.transform.SetParent(gameObject.transform);
        port.GetComponent<PortBehaviour>().SetCoordinate(new Vector3(xPos, yPos, 0));
        port.GetComponent<PortBehaviour>().SetID(ID);
        port.GetComponent<PortBehaviour>().SetPortRole(portType, resourceType);
        _portManager.AddNewPort(ID, port, portType, resourceType);
    }
}
