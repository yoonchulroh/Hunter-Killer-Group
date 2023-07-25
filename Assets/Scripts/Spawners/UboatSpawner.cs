using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class UboatSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _uboatPrefab;

    void Start()
    {
        for (int i = 0; i < 5; ++i)
        {
            SpawnUboat(i, Random.Range(-40, 40), Random.Range(-40, 40));
        }
    }

    private void SpawnUboat(int id, float xPos, float yPos)
    {
        var initialPosition = new Vector3(xPos, yPos, 0);

        var uboat = Instantiate<GameObject>(_uboatPrefab, initialPosition, Quaternion.identity);
        uboat.transform.SetParent(gameObject.transform);
        GameManager.Instance.uboatManager.AddNewUboat(id, uboat);

        uboat.GetComponent<UboatBehaviour>().SetID(id);
        uboat.GetComponent<UboatBehaviour>().SetSpeed(5f);
    }
}
