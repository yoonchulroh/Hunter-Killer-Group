using UnityEngine;
using TMPro;
using System;

public class LabelTextBehaviour : MonoBehaviour
{
    public void SetIdentificationLabel(GameObject parent)
    {
        if (parent.tag == "Port")
        {
            GetComponent<TextMeshPro>().text = parent.GetComponent<PortBehaviour>().alphabetID.ToString();
        }
        else {
            GetComponent<TextMeshPro>().text = parent.GetComponent<MovingEntityBehaviour>().identification;
        }
    }

    public void SetRoleLabel(GameObject parent)
    {
        if (parent.tag == "Port")
        {
            //GetComponent<TextMeshPro>().text = parent.GetComponent<PortBehaviour>().portType + " of " + parent.GetComponent<PortBehaviour>().resourceType;
        }
        else {
            GetComponent<TextMeshPro>().text = parent.GetComponent<MovingEntityBehaviour>().role;
        }
    }

    public void SetResourceNeedLabel(ResourceNeed resourceNeed)
    {
        GetComponent<TextMeshPro>().text = resourceNeed.resourceType.ToString() + " " + Convert.ToString(resourceNeed.amount) + " " + Convert.ToString(Math.Round(Convert.ToDouble(resourceNeed.timeLeft), 0));
    }

    public void SetHpLabel(int hp)
    {
        GetComponent<TextMeshPro>().text = Convert.ToString(hp);
    }

    public void SetEmpty()
    {
        GetComponent<TextMeshPro>().text = "";
    }
}
