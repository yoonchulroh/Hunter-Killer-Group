using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PortSpawner : StationaryEntitySpawner
{
    private PortManager _portManager;

    void Start()
    {
        _portManager = GameManager.Instance.portManager;
        SpawnPort(1, Random.Range(-40, 40), Random.Range(-20, 20), PortType.Producer, ResourceType.Square);
        SpawnPort(2, Random.Range(-40, 40), Random.Range(-20, 20), PortType.Consumer, ResourceType.Square);
        SpawnPort(3, Random.Range(-40, 40), Random.Range(-20, 20), PortType.Consumer, ResourceType.Square);
        SpawnPort(4, Random.Range(-40, 40), Random.Range(-20, 20), PortType.Producer, ResourceType.Star);
        SpawnPort(5, Random.Range(-40, 40), Random.Range(-20, 20), PortType.Consumer, ResourceType.Star);
        SpawnPort(6, Random.Range(-40, 40), Random.Range(-20, 20), PortType.Consumer, ResourceType.Star);
    }

    void SpawnPort(int ID, float xPos, float yPos, PortType portType, ResourceType resourceType)
    {
        var port = SpawnEntity(ID, new Vector3(xPos, yPos, -1));
        _portManager.AddNewPort(ID, port, portType, resourceType);

        port.GetComponent<PortBehaviour>().SetPortRole(portType, resourceType);
    }

    void SpawnPortRandomly(PortType portType, ResourceType resourceType)
    {
        var xLimit = GameManager.Instance.cameraManager.gameObjectXLimit;
        var yLimit = GameManager.Instance.cameraManager.gameObjectYLimit;

        SpawnPort(GameManager.Instance.portManager.portDict.Count, Random.Range(-xLimit, xLimit), Random.Range(-yLimit, yLimit), portType, resourceType);
    }
}
