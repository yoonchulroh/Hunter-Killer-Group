using UnityEngine;

public enum EditMode
{
    CreateConvoys,
    CreateSeawaysOrigin,
    CreateSeawaysDestination,
    CreateRadar,
    Delete
}

public class EditManager : MonoBehaviour
{
    private EditMode _editMode;
    public EditMode editMode => _editMode;
    
    public int seawayOrigin;
    public int seawayDestinationCandidate;

    void Start()
    {
        _editMode = EditMode.CreateSeawaysOrigin;
    }

    public void SwitchEditMode(EditMode target)
    {
        _editMode = target;
    }
}
