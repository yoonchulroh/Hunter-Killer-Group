using UnityEngine;
using TMPro;
using System;

public enum ParentType
    {
        Convoy,
        Port,
        Uboat,
        Escort,
        StationarRadar
    }

public class LabelTextBehaviour : MonoBehaviour
{

    private GameObject _parentObject;
    public GameObject parentObject => _parentObject;

    private ParentType _parentType;
    public ParentType parentType => _parentType;

    public void SetNameLabel(GameObject parent, ParentType parentType)
    {
        _parentObject = parent;
        _parentType = parentType;

        switch(parentType)
        {
            case (ParentType.Convoy):
                GetComponent<TextMeshPro>().text = ((char) (parent.GetComponent<ConvoyBehaviour>().originPortID + 65)).ToString() + ((char) (parent.GetComponent<ConvoyBehaviour>().destinationPortID + 65)).ToString() + " " + parent.GetComponent<ConvoyBehaviour>().id.ToString();
                break;
            case (ParentType.Port):
                GetComponent<TextMeshPro>().text = parent.GetComponent<PortBehaviour>().alphabetID.ToString();
                break;
            case (ParentType.Uboat):
                GetComponent<TextMeshPro>().text = "U-" + parent.GetComponent<UboatBehaviour>().id.ToString();
                break;
            case (ParentType.Escort):
                GetComponent<TextMeshPro>().text = "DD-" + parent.GetComponent<EscortBehaviour>().id.ToString();
                break;
            default:
                break;
        }
    }

    public void SetRoleLabel(GameObject parent, ParentType parentType)
    {
        switch(parentType)
        {
            case (ParentType.Convoy):
                GetComponent<TextMeshPro>().text = "Carrying " + Convert.ToString(parent.GetComponent<ConvoyBehaviour>().resourceAmount) + " " + parent.GetComponent<ConvoyBehaviour>().resourceType;
                break;
            case (ParentType.Port):
                GetComponent<TextMeshPro>().text = parent.GetComponent<PortBehaviour>().portType + " of " + parent.GetComponent<PortBehaviour>().resourceType;
                break;
            case (ParentType.Uboat):
                GetComponent<TextMeshPro>().text = "Type VII";
                break;
            case (ParentType.Escort):
                GetComponent<TextMeshPro>().text = "Clemson-class";
                break;
            default:
                break;
        }
    }

    public void SetResourceAmountLabel(float resourceAmount)
    {
        GetComponent<TextMeshPro>().text = Convert.ToString(Math.Round(Convert.ToDouble(resourceAmount), 0));
    }

    public void SetHpLabel(int hp)
    {
        GetComponent<TextMeshPro>().text = Convert.ToString(hp);
    }
}
