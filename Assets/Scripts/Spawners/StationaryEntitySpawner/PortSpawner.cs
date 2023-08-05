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
        SpawnPortRandomly(2, PortType.Producer, ResourceType.Green);
        SpawnPortRandomly(3, PortType.Producer, ResourceType.Blue);
        SpawnPortRandomly(4, PortType.Producer, ResourceType.Yellow);

        StartCoroutine(SpawnPortPeriodically());
    }

    void SpawnPort(int ID, float xPos, float yPos, PortType portType, ResourceType resourceType = ResourceType.White)
    {
        GameObject port = null;
        if (portType == PortType.Producer)
        {
            port = SpawnEntity(ID, new Vector3(xPos, yPos, -1));
            port.GetComponent<ProducerPortBehaviour>().SetPortRole(resourceType);
        } else {
            port = SpawnEntity(ID, new Vector3(xPos, yPos, -1), true);
        }

        _portManager.AddNewPort(ID, port, portType, resourceType);
    }

    void SpawnPortRandomly(int id, PortType portType, ResourceType resourceType = ResourceType.White)
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

    private IEnumerator SpawnPortPeriodically()
    {
        var i = 5;
        while (true)
        {
            SpawnPortRandomly(i, PortType.Consumer);
            i += 1;
            yield return new WaitForSeconds(20f);
        }
    }
}
