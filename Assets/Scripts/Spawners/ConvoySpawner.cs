using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ConvoySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _portSpawner;
    [SerializeField] private GameObject _convoyPrefab;

    private void SpawnConvoy(int convoyID, int originID, int destinationID, ResourceType resourceType)
    {
        var convoy = Instantiate<GameObject>(_convoyPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        convoy.transform.SetParent(gameObject.transform);
        GameManager.Instance.convoyManager.AddNewConvoy(convoyID, convoy);

        convoy.GetComponent<ConvoyBehaviour>().SetOrigin(GameManager.Instance.portManager.portDict[originID]);
        convoy.GetComponent<ConvoyBehaviour>().SetSpeed(5f);
        convoy.GetComponent<ConvoyBehaviour>().SetDestination(GameManager.Instance.portManager.portDict[destinationID]);
        convoy.GetComponent<ConvoyBehaviour>().SetID(convoyID);
        convoy.GetComponent<ConvoyBehaviour>().SetRole(resourceType);
    }

    public void SpawnConvoyOnPort(int originID, ResourceType resourceType)
    {
        var portCount = GameManager.Instance.portManager.portDict.Count;
        var convoyCount = GameManager.Instance.convoyManager.convoyDict.Count;
        var destinationID = GameManager.Instance.portManager.consumerPortDict[resourceType][Random.Range(0, GameManager.Instance.portManager.consumerPortDict[resourceType].Count)];
        SpawnConvoy(convoyCount, originID, destinationID, resourceType);
    }
}
