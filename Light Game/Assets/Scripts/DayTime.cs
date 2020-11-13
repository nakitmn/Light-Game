using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DayTime : MonoBehaviour
{
    [SerializeField] Gradient directionalLightGradient;
    [SerializeField] Gradient ambientLightGradient;

    [SerializeField] private float timeDaySeconds = 60;
    [SerializeField, Range(0f, 1f)] float timeProgress;

    [SerializeField] Light directionalLight;

    Vector3 defaultAngle;

    public static DayTime instance;
    public float TimeProgress { get => timeProgress; set => timeProgress = value; }

    void Start()
    {
        defaultAngle = directionalLight.transform.localEulerAngles;
        instance = this;
    }

    public delegate void OnTimeChange(float timeValue);
    public static event OnTimeChange onTimeChange;

    // Update is called once per frame
    void Update()
    {
        if (Application.isPlaying)
        {
            timeProgress += Time.deltaTime / timeDaySeconds; // Двигаем наше время
            if (timeProgress > 1f) timeProgress = 0f; 
            onTimeChange.Invoke(timeProgress);
        }

        directionalLight.color = directionalLightGradient.Evaluate(timeProgress);
        RenderSettings.ambientLight = ambientLightGradient.Evaluate(timeProgress);

        directionalLight.transform.localEulerAngles = new Vector3(360 * timeProgress - 90, defaultAngle.x, defaultAngle.z);
    }

    // To enable slider value changes
    public void ChangeTimeSpeed(float newTime)
    {
        timeDaySeconds = newTime;
    }
}
