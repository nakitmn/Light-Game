using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorMode : Behaviour
{
    [SerializeField] float enableLightDuration = 10f;
    GameObject[] roomLights;
    protected override void Init()
    {
        base.Init();
        roomLights = GameObject.FindGameObjectsWithTag("RoomLight");
        foreach (GameObject item in roomLights)
        {
            item.GetComponent<LightController>().StartSensorMode(enableLightDuration);
        }
    }

    public override void Stop()
    {
        if (isOn)
        {
            foreach (GameObject item in roomLights)
            {
                item.GetComponent<LightController>().SensorMode=false;
            }
            base.Stop();
        }
    }
}
