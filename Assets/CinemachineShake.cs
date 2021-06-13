using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineShake : MonoBehaviour
{
    //https://www.youtube.com/watch?v=ACf1I27I6Tk

    CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;
    CinemachineVirtualCamera cinemachineVirtualCamera;
    private void Awake()
    {
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }
    public void ShakeCamera(float Intensity, float time)
    {
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Intensity;
        Invoke("StopCameraShake", time);
    }
    void StopCameraShake()
    {
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;
    }
}
