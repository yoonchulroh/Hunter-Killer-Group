using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ConvoySpawner : MovingEntitySpawner
{
    private void SpawnConvoy(int convoyID, int originID, int destinationID, ResourceType resourceType)
    {
        var convoy = SpawnEntity(new Vector3(0, 0, 0), convoyID, 50, 2f, 0, 5f, 0);
        GameManager.Instance.convoyManager.AddNewConvoy(convoyID, convoy);

        convoy.GetComponent<ConvoyBehaviour>().SetOrigin(GameManager.Instance.portManager.portDict[originID]);
        convoy.GetComponent<ConvoyBehaviour>().SetDestination(GameManager.Instance.portManager.portDict[destinationID]);
        convoy.GetComponent<ConvoyBehaviour>().SetRole(resourceType);
    }

    public void SpawnConvoyOnPort(int originID, ResourceType resourceType)
    {
        var portCount = GameManager.Instance.portManager.portDict.Count;
        var convoyCount = GameManager.Instance.convoyManager.convoyDict.Count;
        var destinationID = GameManager.Instance.portManager.consumerPortDict[resourceType][Random.Range(0, GameManager.Instance.portManager.consumerPortDict[resourceType].Count)];
        SpawnConvoy(convoyCount + 1, originID, destinationID, resourceType);
    }
}
