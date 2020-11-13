using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalLightController : MonoBehaviour
{
    [SerializeField] Light mainRoomLight;
    Light lightComponent;

    // Start is called before the first frame update
    void Start()
    {
        lightComponent = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        lightComponent.enabled = mainRoomLight.enabled;
    }
}
