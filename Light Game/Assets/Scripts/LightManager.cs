//using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LightManager : MonoBehaviour
{
    public Behaviour currentBehaviourMode;

    void Update()
    {
        currentBehaviourMode.Play();
    }

    public void ChangeMode(int id)
    {
        switch (id)
        {
            case 0:
                {
                    currentBehaviourMode = GetComponent<DefaultMode>();
                    break;
                }
            case 1:
                {
                    currentBehaviourMode = GetComponent<SensorMode>();
                    break;
                }
            case 2:
                {
                    currentBehaviourMode = GetComponent<GuestsMode>();
                    break;
                }
            case 3:
                {
                    currentBehaviourMode = GetComponent<OutsideMode>();
                    break;
                }
            default:
                {
                    currentBehaviourMode = GetComponent<DefaultMode>();
                    break;
                }
        }
    }
}