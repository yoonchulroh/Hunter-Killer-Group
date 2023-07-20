using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvoySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _portSpawner;
    [SerializeField] private GameObject _convoyPrefab;
    private bool convoyCreated = false;

    void Start()
    {
        Invoke("SpawnConvoy", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnConvoy()
    {
        if (!convoyCreated)
        {
            try
            {
                var convoy = Instantiate<GameObject>(_convoyPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                GetComponent<ConvoyManager>().AddNewConvoy(0, convoy);
                convoy.GetComponent<ConvoyBehaviour>().SetOrigin(_portSpawner.GetComponent<PortManager>().portDict[0]);
                convoy.GetComponent<ConvoyBehaviour>().SetDestination(_portSpawner.GetComponent<PortManager>().portDict[1]);
                convoy.GetComponent<ConvoyBehaviour>().SetSpeed(5f);
                convoyCreated = true;
            }
            catch
            {}
        }
    }
}
