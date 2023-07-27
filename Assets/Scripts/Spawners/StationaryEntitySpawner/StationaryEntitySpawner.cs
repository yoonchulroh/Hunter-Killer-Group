using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class StationaryEntitySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _entityPrefab;

    protected GameObject SpawnEntity(int id, Vector3 position)
    {
        var entity = Instantiate<GameObject>(_entityPrefab, position, Quaternion.identity);

        entity.transform.SetParent(gameObject.transform);
        
        entity.GetComponent<StationaryEntityBehaviour>().SetID(id);
        entity.GetComponent<StationaryEntityBehaviour>().SetCoordinate(position.x, position.y);

        return entity;
    }

    public GameObject SpawnEntityRandomly(int id)
    {
        var xLimit = GameManager.Instance.cameraManager.gameObjectXLimit;
        var yLimit = GameManager.Instance.cameraManager.gameObjectYLimit;

        var entity = SpawnEntity(id, new Vector3(Random.Range(-xLimit, xLimit), Random.Range(-yLimit, yLimit), -1));
        return entity;
    }
}
