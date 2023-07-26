using System;
using UnityEngine;
using TMPro;

public enum EditMode
{
    CreateConvoys,
    CreateSeawaysOrigin,
    CreateSeawaysDestination,
    CreateRadar,
    CreateEscort,
    Delete,
    Select,
    None
}

public class EditManager : MonoBehaviour
{
    [SerializeField] private GameObject _identificationField;
    [SerializeField] private GameObject _hpField;
    [SerializeField] private GameObject _roleField;

    private EditMode _editMode;
    public EditMode editMode => _editMode;
    
    public int seawayOrigin;
    public int seawayDestinationCandidate;

    public GameObject selectedObject;

    void Start()
    {
        _editMode = EditMode.CreateSeawaysOrigin;
    }

    public void SwitchEditMode(EditMode target, GameObject selectedObject = null)
    {
        _editMode = target;
        if (target == EditMode.Select && selectedObject != null)
        {
            Select(selectedObject);
        }
        else {
            Unselect();
        }
    }

    private void Select(GameObject selectedObject)
    {
        _identificationField.GetComponent<TextMeshProUGUI>().text = selectedObject.GetComponent<MovingEntityBehaviour>().identification;
        _hpField.GetComponent<TextMeshProUGUI>().text = Convert.ToString(selectedObject.GetComponent<MovingEntityBehaviour>().movingEntityData.hp);
        _roleField.GetComponent<TextMeshProUGUI>().text = selectedObject.GetComponent<MovingEntityBehaviour>().role;
    }

    private void Unselect()
    {
        _identificationField.GetComponent<TextMeshProUGUI>().text = "";
        _hpField.GetComponent<TextMeshProUGUI>().text = "";
        _roleField.GetComponent<TextMeshProUGUI>().text = "";
    }
}
