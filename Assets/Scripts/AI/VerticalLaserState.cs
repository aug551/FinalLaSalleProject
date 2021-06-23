using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalLaserState : IState
{
    public float laserStartSize = 0.1f;
    [SerializeField] float maxWidth1;
    [SerializeField] float maxWidth2;

    public VerticalLaserState() //constructor
    {
        canTransition = false;
    }

    public override IEnumerator Enter(List<StationaryLaser> stationaryLasers)
    {
        canTransition = false;
        Initlaser(stationaryLasers);
        ActivateTheLasers(maxWidth1, stationaryLasers);
        yield return new WaitForSeconds(3f);
        theBoss.cinemachineShake.ShakeCamera(0.6f, 1.8f);
        ActivateTheLasers(maxWidth2, stationaryLasers);
        yield return new WaitForSeconds(3f);
        Finishedlaser(stationaryLasers);
        canTransition = true;
        yield break;
    }

    public void Finishedlaser(List<StationaryLaser> stationaryLasers)
    {
        foreach (StationaryLaser stationaryLaser in stationaryLasers)
        {
            theBoss.LineRenderer.enabled = true;
        }

    }

    public void Initlaser(List<StationaryLaser> stationaryLasers)
    {
        foreach (StationaryLaser stationaryLaser in stationaryLasers)
        {
            theBoss.LineRenderer.enabled = true; 
        }
    }

    public void ActivateTheLasers(float maxsize, List<StationaryLaser> stationaryLasers)
    {
        foreach(StationaryLaser stationaryLaser in stationaryLasers)
        {
            stationaryLaser.IncreaseLaserSize(maxsize);
        }
    }
}
