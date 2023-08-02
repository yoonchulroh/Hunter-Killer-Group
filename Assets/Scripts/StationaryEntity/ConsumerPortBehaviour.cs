using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ConsumerPortBehaviour : PortBehaviour
{
    private List<ResourceNeed> _resourceNeedList = new List<ResourceNeed>();
    public List<ResourceNeed> resourceNeedList => _resourceNeedList;

    private float _convoySpawnPeriod = 10f;
    private float _needGenerationPeriod = 30f;

    private GameObject _resourceNeedLabel;

    void Start()
    {
        var resourceNeedLabel = Instantiate<GameObject>(_labelPrefab, new Vector3(3, 0, 0), Quaternion.identity);
        resourceNeedLabel.transform.SetParent(gameObject.transform, false);

        _resourceNeedLabel = resourceNeedLabel;

        StartCoroutine(SpawnConvoyPeriodically());
        StartCoroutine(GenerateNeedPeriodically());
    }

    void Update()
    {
        foreach (ResourceNeed need in resourceNeedList)
        {
            need.ReduceTimeLeft(Time.deltaTime);
        }

        if (resourceNeedList.Count > 0)
        {
            _resourceNeedLabel.GetComponent<LabelTextBehaviour>().SetResourceNeedLabel(resourceNeedList[0]);
        } else {
            _resourceNeedLabel.GetComponent<LabelTextBehaviour>().SetEmpty();
        }
    }

    public void ResourceFilled(ResourceType resourceType, int amount)
    {
        int amountLeft = amount;

        foreach (ResourceNeed need in new List<ResourceNeed>(resourceNeedList))
        {
            if (need.resourceType == resourceType)
            {
                amountLeft = need.ReduceAmountLeft(amountLeft);
                if (need.amount <= 0)
                {
                    resourceNeedList.Remove(need);
                }
                if (amountLeft <= 0)
                {
                    break;
                }
            }
        }
    }

    private IEnumerator SpawnConvoyPeriodically()
    {
        while (true)
        {
            foreach (ResourceNeed need in resourceNeedList)
            {
                if (GameManager.Instance.portManager.FindNextPort(_id, GameManager.Instance.portManager.producerPortDict[need.resourceType]) > 0)
                {
                    _convoySpawner.GetComponent<ConvoySpawner>().SpawnConvoyOnPort(GameManager.Instance.portManager.producerPortDict[need.resourceType], _id, need.resourceType);
                }
            }
            yield return new WaitForSeconds(_convoySpawnPeriod);
        }
    }

    private IEnumerator GenerateNeedPeriodically()
    {
        while (true)
        {
            _resourceNeedList.Add(new ResourceNeed((ResourceType)Random.Range(1, 4), 100, 60f));
            yield return new WaitForSeconds(_needGenerationPeriod);
        }
    }
}

public class ResourceNeed
{
    private ResourceType _resourceType;
    public ResourceType resourceType => _resourceType;

    private int _amount;
    public int amount => _amount;

    private float _timeLeft;
    public float timeLeft => _timeLeft;

    public ResourceNeed(ResourceType resourceType, int amount, float timeLeft)
    {
        _resourceType = resourceType;
        _amount = amount;
        _timeLeft = timeLeft;
    }

    public void ReduceTimeLeft(float seconds)
    {
        _timeLeft -= seconds;
        if (_timeLeft < 0)
        {
            _timeLeft = 0;
        }
    }

    public int ReduceAmountLeft(int change)
    {
        if (_amount > change)
        {
            _amount -= change;
            return 0;
        }
        else
        {
            var changeUnused = change - _amount;
            _amount = 0;
            return changeUnused;
        }
    }
}
