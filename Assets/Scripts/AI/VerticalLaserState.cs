using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalLaserState : IState
{
    public float laserStartSize = 0.1f;
    [SerializeField] float maxWidth1 = 0.1f;
    [SerializeField] float maxWidth2 = 2f;
    List<StationaryLaser> lasers = new List<StationaryLaser>();

    public VerticalLaserState(LaserRoom room) //constructor
    {
        laserRoom = room;
        canTransition = false;
    }

    public override IEnumerator Enter()
    {
        canTransition = false;
        lasers = laserRoom.GetLaserTurrets(new int[] { 3, 8 });
        Initlaser(lasers);
        ActivateTheLasers(maxWidth1, lasers);
        yield return new WaitForSeconds(3f);
        ActivateTheLasers(maxWidth2, lasers);
        yield return new WaitForSeconds(3f);
        lasers = laserRoom.GetLaserTurrets(new int[] { 1, 4 });
        Initlaser(lasers);
        ActivateTheLasers(maxWidth1, lasers);
        yield return new WaitForSeconds(3f);
        ActivateTheLasers(maxWidth2, lasers);
        yield return new WaitForSeconds(3f);
        lasers = laserRoom.GetLaserTurrets(new int[] { 1, 4 });
        Initlaser(lasers);
        ActivateTheLasers(maxWidth1, lasers);
        yield return new WaitForSeconds(3f);
        ActivateTheLasers(maxWidth2, lasers);
        yield return new WaitForSeconds(3f);
        Finishedlaser(lasers);
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
            stationaryLaser.Line.enabled = true; 
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
