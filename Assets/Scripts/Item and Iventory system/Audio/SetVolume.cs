using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{
    public AudioMixer audioM;

    public void SetMasterVol(float level)
    {
        audioM.SetFloat("masterVol", level);
    }

    public void SetMusicVol(float level)
    {
        audioM.SetFloat("musicVol", level);
    }

    public void SetSFXVol(float level)
    {
        audioM.SetFloat("sfxVol", level);
    }
}
