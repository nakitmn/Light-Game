using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public bool playerIsIn = false;
    public bool SensorMode { get; set; }
    float _lightEnabledDuration;
    TimeSpan turnOffLightTime;

    Light lightComponent;

    // Start is called before the first frame update
    void Start()
    {
        lightComponent = gameObject.GetComponent<Light>();
    }

    public delegate void OnRoomEnter(bool lightState);
    public static event OnRoomEnter onRoomEnter;

    public delegate void OnRoomLeave();
    public static event OnRoomLeave onRoomLeave;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsIn = true;
            if (SensorMode)
            {
                lightComponent.enabled = true;
                TurnOffLight();
            }
            onRoomEnter.Invoke(lightComponent.enabled);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsIn = false;
            onRoomLeave.Invoke();
        }
    }

    public void ChangeState(bool state)
    {
        lightComponent.enabled = playerIsIn ? state : lightComponent.enabled;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsIn)
        {
            lightComponent.enabled = !lightComponent.enabled;
            onRoomEnter.Invoke(lightComponent.enabled);
        }
        if (SensorMode) TurnOnLightOnMove();
    }

    private void TurnOnLightOnMove()
    {
        if (playerIsIn && lightComponent.enabled==false)
        {
            if (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f)
            {
                lightComponent.enabled = true;
                onRoomEnter.Invoke(lightComponent.enabled);
                TurnOffLight();
            }
        }
    }

    public void StartSensorMode(float lightEnabledDuration)
    {
        SensorMode = true;
        _lightEnabledDuration = lightEnabledDuration;
    }

    private void TurnOffLight()
    {
        turnOffLightTime = TimeSpan.FromDays(DayTime.instance.TimeProgress).Add(TimeSpan.FromMinutes(_lightEnabledDuration));
        if (turnOffLightTime.TotalDays == 1) turnOffLightTime = TimeSpan.FromDays(0);
        DayTime.onTimeChange += CheckForTurnOffLight;
    }

    public void CheckForTurnOffLight(float time)
    {
        if (SensorMode == false) { DayTime.onTimeChange -= CheckForTurnOffLight; return; }
        TimeSpan currentTime = TimeSpan.FromDays(time);
        if (currentTime >= turnOffLightTime)
        {
            lightComponent.enabled = false;
            onRoomEnter.Invoke(lightComponent.enabled);
            DayTime.onTimeChange -= CheckForTurnOffLight;
        }
    }
}
