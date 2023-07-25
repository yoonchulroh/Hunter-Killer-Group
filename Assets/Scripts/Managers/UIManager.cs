using UnityEngine;

public class UIManager : MonoBehaviour
{
    private EditManager _editManager;

    void Start() 
    {
        _editManager = GameManager.Instance.editManager;
    }

    public void OnClickConvoyButton()
    {
        _editManager.SwitchEditMode(EditMode.CreateConvoys);
    }

    public void OnClickSeawayButton()
    {
        _editManager.SwitchEditMode(EditMode.CreateSeawaysOrigin);
    }

    public void OnClickRadarButton()
    {
        _editManager.SwitchEditMode(EditMode.CreateRadar);
    }

    public void OnClickDeleteButton()
    {
        _editManager.SwitchEditMode(EditMode.Delete);
    }
}
