using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ConvoySpawner : MovingEntitySpawner
{
    private void SpawnConvoy(int convoyID, int originID, int destinationID, ResourceType resourceType)
    {
        if (GameManager.Instance.industryManager.UseIndustrialCapacity(10))
        {
            var convoy = SpawnEntity(new Vector3(0, 0, 0), MovingEntityInitialData.Convoy(convoyID));
            GameManager.Instance.convoyManager.AddNewEntity(convoyID, convoy);

            convoy.GetComponent<ConvoyBehaviour>().SetOrigin(GameManager.Instance.portManager.portDict[originID]);
            convoy.GetComponent<ConvoyBehaviour>().SetDestination(GameManager.Instance.portManager.portDict[destinationID]);
            convoy.GetComponent<ConvoyBehaviour>().SetRole(resourceType);
        }
    }

    public void SpawnConvoyOnPort(int originID, int destinationID, ResourceType resourceType)
    {
        var portCount = GameManager.Instance.portManager.portDict.Count;
        var convoyCount = GameManager.Instance.convoyManager.movingEntityDict.Count;
        SpawnConvoy(convoyCount + 1, originID, destinationID, resourceType);
    }
}
