using UnityEngine;

public class UIManager : MonoBehaviour
{
    private EditManager _editManager;
    private TimeManager _timeManager;

    void Start() 
    {
        _editManager = GameManager.Instance.editManager;
        _timeManager = GameManager.Instance.timeManager;
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

    public void OnClickEscortButton()
    {
        _editManager.SwitchEditMode(EditMode.CreateEscort);
    }

    public void OnClickDeleteButton()
    {
        _editManager.SwitchEditMode(EditMode.Delete);
    }

    public void OnClickPlayPauseButton()
    {
        _timeManager.PlayPause();
    }
}
