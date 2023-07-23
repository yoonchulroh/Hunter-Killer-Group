using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PortSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _portPrefab;
    [SerializeField] private GameObject _labelPrefab;
    private PortManager _portManager;

    void Start()
    {
        _portManager = GameManager.Instance.portManager;
        for (int i = 0; i < 5; ++i)
        {
            SpawnPort(i, Random.Range(-40, 40), Random.Range(-20, 20));
            //SpawnPort(i, i*10 - 20, i*5 - 10);
        }
    }

    void SpawnPort(int ID, float xPos, float yPos)
    {
        var port = Instantiate<GameObject>(_portPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        port.transform.SetParent(gameObject.transform);
        port.GetComponent<PortBehaviour>().SetCoordinate(new Vector3(xPos, yPos, 0));
        port.GetComponent<PortBehaviour>().SetID(ID);
        _portManager.AddNewPort(ID, port);

        var labelText = Instantiate<GameObject>(_labelPrefab, new Vector3(0, 1, 0), Quaternion.identity);
        labelText.GetComponent<LabelTextBehaviour>().SetParentType(port, ParentType.Port);
    }
}
