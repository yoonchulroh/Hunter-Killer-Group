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
        SpawnPortRandomly(1, PortType.Producer, ResourceType.Red);
        SpawnPortRandomly(2, PortType.Consumer, ResourceType.Red);
        SpawnPortRandomly(3, PortType.Consumer, ResourceType.Red);
        SpawnPortRandomly(4, PortType.Producer, ResourceType.Blue);
        SpawnPortRandomly(5, PortType.Consumer, ResourceType.Blue);
        SpawnPortRandomly(6, PortType.Consumer, ResourceType.Blue);
        SpawnPortRandomly(7, PortType.Producer, ResourceType.Green);
        SpawnPortRandomly(8, PortType.Consumer, ResourceType.Green);
        SpawnPortRandomly(9, PortType.Consumer, ResourceType.Green);
        SpawnPortRandomly(10, PortType.Producer, ResourceType.Yellow);
        SpawnPortRandomly(11, PortType.Consumer, ResourceType.Yellow);
        SpawnPortRandomly(12, PortType.Consumer, ResourceType.Yellow);
    }

    void SpawnPort(int ID, float xPos, float yPos, PortType portType, ResourceType resourceType)
    {
        var port = SpawnEntity(ID, new Vector3(xPos, yPos, -1));
        _portManager.AddNewPort(ID, port, portType, resourceType);

        port.GetComponent<PortBehaviour>().SetPortRole(portType, resourceType);
    }

    void SpawnPortRandomly(int id, PortType portType, ResourceType resourceType)
    {
        var xLimit = GameManager.Instance.cameraManager.gameObjectXLimit;
        var yLimit = GameManager.Instance.cameraManager.gameObjectYLimit;

        var portExistsInRange = true;
        float xPos = 0;
        float yPos = 0;
        while (portExistsInRange)
        {
            xPos = Random.Range(-xLimit, xLimit);
            yPos = Random.Range(-yLimit, yLimit);
            portExistsInRange = GameManager.Instance.portManager.PortInRange(xPos, yPos, 4f);
        }
        SpawnPort(id, xPos, yPos, portType, resourceType);
    }
}
