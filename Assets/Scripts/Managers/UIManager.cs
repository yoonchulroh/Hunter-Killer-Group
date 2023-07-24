using UnityEngine;

public class UIManager : MonoBehaviour
{
    private CreateManager _createManager;

    void Start() 
    {
        _createManager = GameManager.Instance.createManager;
    }

    public void OnClickConvoyButton()
    {
        _createManager.SwitchCreateMode(CreateMode.Convoys);
    }

    public void OnClickSeawayButton()
    {
        _createManager.SwitchCreateMode(CreateMode.SeawaysOrigin);
    }

    public void OnClickRadarButton()
    {
        _createManager.SwitchCreateMode(CreateMode.Radar);
    }
}
