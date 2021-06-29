using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineShake : MonoBehaviour
{
    //https://www.youtube.com/watch?v=ACf1I27I6Tk

    CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;
    CinemachineVirtualCamera cinemachineVirtualCamera;
    Animator anim;
    private void Awake()
    {
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        anim = GetComponent<Animator>();
    }
    public void ShakeCamera(float Intensity, float time)
    {
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Intensity;
        Invoke("StopCameraShake", time);
    }

    public void MoveCamera(bool boolean)
    {
        anim.SetBool("MoveDown", boolean);
    }
    void StopCameraShake()
    {
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;
    }
}
