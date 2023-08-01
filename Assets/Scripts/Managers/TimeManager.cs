using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private enum TimeStatus
    {
        Paused,
        Speed1
    }

    private TimeStatus _timeStatus;

    void Start()
    {
        Time.timeScale = 0f;
        _timeStatus = TimeStatus.Paused;
    }

    public void PlayPause()
    {
        if (_timeStatus != TimeStatus.Paused)
        {
            Time.timeScale = 0f;
            _timeStatus = TimeStatus.Paused;
        }
        else {
            Time.timeScale = 1f;
            _timeStatus = TimeStatus.Speed1;
        }
    }
}
