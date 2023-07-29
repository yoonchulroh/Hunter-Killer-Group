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
    None
}

public class EditManager : MonoBehaviour
{
    [SerializeField] private GameObject _identificationField;
    [SerializeField] private GameObject _hpField;
    [SerializeField] private GameObject _roleField;
    [SerializeField] private GameObject _statusField;

    private EditMode _editMode;
    public EditMode editMode => _editMode;
    
    public int seawayOrigin;
    public int seawayDestinationCandidate;

    public GameObject selectedObject = null;

    void Start()
    {
        _editMode = EditMode.None;
    }

    void Update()
    {
        if (selectedObject != null)
        {
            _identificationField.GetComponent<TextMeshProUGUI>().text = selectedObject.GetComponent<MovingEntityBehaviour>().identification;
            _hpField.GetComponent<TextMeshProUGUI>().text = Convert.ToString(selectedObject.GetComponent<MovingEntityBehaviour>().movingEntityData.hp);
            _roleField.GetComponent<TextMeshProUGUI>().text = selectedObject.GetComponent<MovingEntityBehaviour>().role;
            if (selectedObject.tag == "Escort")
            {
                _statusField.GetComponent<TextMeshProUGUI>().text = selectedObject.GetComponent<EscortBehaviour>().currentStatus;
            } else {
                _statusField.GetComponent<TextMeshProUGUI>().text = "";
            }
        } else {
            _identificationField.GetComponent<TextMeshProUGUI>().text = "";
            _hpField.GetComponent<TextMeshProUGUI>().text = "";
            _roleField.GetComponent<TextMeshProUGUI>().text = "";
            _statusField.GetComponent<TextMeshProUGUI>().text = "";
        }
    }

    public void SwitchEditMode(EditMode target)
    {
        _editMode = target;
    }
}
