using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideMode : Behaviour
{
    [SerializeField] private float lightEnabledDuration = 30f;

    // Random
    private System.Random random = new System.Random();
    // All lights on the scene
    private Light[] lights;
    // Chosen light by random among all lights
    private Light currentLight;
    private bool timer;

    TimeSpan endTime, currentTime;

    protected override void Init()
    {
        base.Init();
        lights = GetAllLights();
        currentLight = lights[random.Next(lights.Length)];
        endTime = GetCurrentTimeProgress().Add(TimeSpan.FromMinutes(lightEnabledDuration));
        timer = true;
    }

    public override void Play()
    {
        if (!isOn)
        {
            Init();
        }
        if (timer)
        {
            currentTime = GetCurrentTimeProgress();
            if (endTime <= currentTime)
            {
                timer = false;
            }
        }
        if (!timer)
        {
            endTime = GetCurrentTimeProgress().Add(TimeSpan.FromMinutes(lightEnabledDuration));
            if (endTime.TotalDays >= 1) // Just wait for new day
                timer = false;
            else
            {
                TurnOffLight();
                EnableOtherLight();
                timer = true;
            }
        }
    }

    private void EnableOtherLight()
    {
        currentLight = lights[random.Next(lights.Length)];
        currentLight.enabled = true;
        Debug.Log(currentLight.gameObject.name + " is on");
    }

    private void TurnOffLight()
    {
        currentLight.enabled = false;
    }

    private TimeSpan GetCurrentTimeProgress()
    {
        return TimeSpan.FromDays(DayTime.instance.TimeProgress);
    }
}