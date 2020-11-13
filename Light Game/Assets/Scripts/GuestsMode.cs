using UnityEngine;


public class GuestsMode : Behaviour
{
    public AudioSource music;
    protected override void Init()
    {
        base.Init();
        foreach (Light item in GetAllLights())
        {
            item.enabled = true;
        }
        if(!music.isPlaying) music.Play();
    }

    public override void Stop()
    {
        base.Stop();
        if (music.isPlaying) music.Stop();
    }
}

