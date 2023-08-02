using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProducerPortBehaviour : PortBehaviour
{
    private ResourceType _resourceType;
    public ResourceType resourceType => _resourceType;

    void Start()
    {
        transform.localScale = new Vector3(2, 2, 2);
    }

    public void SetPortRole(ResourceType resourceType)
    {
        _resourceType = resourceType;
        GetComponent<SpriteRenderer>().color = ResourceData.ResourceTypeToColor(resourceType);
    }
}
