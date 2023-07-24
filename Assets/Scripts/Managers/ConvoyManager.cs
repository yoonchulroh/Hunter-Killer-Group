using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvoyManager : MonoBehaviour
{
    private Dictionary<int, GameObject> _convoyDict = new Dictionary<int, GameObject>();
    public Dictionary<int, GameObject> convoyDict => _convoyDict;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddNewConvoy(int id, GameObject convoy)
    {
        _convoyDict.Add(id, convoy);
    }
}
