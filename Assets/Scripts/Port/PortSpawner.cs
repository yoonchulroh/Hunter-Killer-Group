using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _portPrefab;
    void Start()
    {
        var port = Instantiate<GameObject>(_portPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        port.GetComponent<PortBehaviour>().SetCoordinate(new Vector3(-30, 0, 0));
        port.GetComponent<PortBehaviour>().SetID(0);
        GetComponent<PortManager>().AddNewPort(0, port);

        port = Instantiate<GameObject>(_portPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        port.GetComponent<PortBehaviour>().SetCoordinate(new Vector3(30, 0, 0));
        port.GetComponent<PortBehaviour>().SetID(1);
        GetComponent<PortManager>().AddNewPort(1, port);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
