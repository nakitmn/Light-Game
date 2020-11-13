using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("Time")]
    [SerializeField] Text currentTimeLabel;
    [SerializeField] Slider timeSpeedSlider;

    [SerializeField] Toggle lightStateToggle;

    [SerializeField] string timeFormat = "Time: {0}";

    // Start is called before the first frame update
    void Start()
    {
        //timeSpeedSlider.value = GameObject.Find("DayNightSystem").GetComponent<DayTime>().timeDaySeconds;
        DayTime.onTimeChange += PrintTime;
        LightController.onRoomEnter += PrintLightState;
        LightController.onRoomLeave += HideLightStateToggle;
    }

    private void PrintTime(float timeValue)
    {
        TimeSpan timeSpan = TimeSpan.FromDays(timeValue);
        currentTimeLabel.text = String.Format(timeFormat,timeSpan.ToString("hh' : 'mm"));
    }

    public void PrintLightState(bool lightState)
    {
        lightStateToggle.isOn = lightState;
        lightStateToggle.gameObject.SetActive(true);
    }

    public void HideLightStateToggle()
    {
        lightStateToggle.gameObject.SetActive(false);
    }

    public void CloseApplication()
    {
        Application.Quit();
    }
}
