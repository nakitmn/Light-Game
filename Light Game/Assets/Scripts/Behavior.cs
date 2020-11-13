using UnityEngine;

public class Behaviour : MonoBehaviour
{
    protected bool isOn = false;

    public Light[] GetAllLights()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("RoomLight");
        Light[] lights = new Light[gameObjects.Length];
        for (int i = 0; i < gameObjects.Length; i++)
        {
            lights[i] = gameObjects[i].GetComponent<Light>();
            Debug.Log(gameObjects[i].name);
        }
        return lights;
    }

    public void StopAllModes()
    {
        Behaviour[] allModes = GameObject.FindObjectsOfType<Behaviour>();
        foreach (Behaviour behaviour in allModes)
        {
            behaviour.Stop();
        }
        Debug.Log("All modes are turned off");
    }

    public virtual void Play()
    {
        if (isOn) return;
        else Init();
    }

    public virtual void Stop() => isOn = false;

    protected virtual void Init() { StopAllModes(); isOn = true; }
}