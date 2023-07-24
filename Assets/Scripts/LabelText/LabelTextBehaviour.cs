using UnityEngine;
using TMPro;

public enum ParentType
    {
        Convoy,
        Port
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
            default:
                break;
        }
    }

    public void SetRoleLabel(GameObject parent, ParentType parentType)
    {
        switch(parentType)
        {
            case (ParentType.Convoy):
                GetComponent<TextMeshPro>().text = "Carrying " + parent.GetComponent<ConvoyBehaviour>().resourceType;
                break;
            case (ParentType.Port):
                GetComponent<TextMeshPro>().text = parent.GetComponent<PortBehaviour>().portType + " of " + parent.GetComponent<PortBehaviour>().resourceType;
                break;
            default:
                break;
        }
    }
}
