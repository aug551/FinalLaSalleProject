using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalLaserState : IState
{
    public float laserStartSize = 0.1f;
    [SerializeField] float maxWidth1 = 0.1f;
    [SerializeField] float maxWidth2 = 2f;
    List<StationaryLaser> lasers = new List<StationaryLaser>();
    bool laserRoomFinished;

    public VerticalLaserState(LaserRoom room, TheBoss boss) //constructor
    {
        theBoss = boss;
        laserRoom = room;
        canTransition = false;
    }

    public override IEnumerator Enter()
    {
        //17 lasers total
        canTransition = false;

        lasers = laserRoom.GetLaserTurrets(new int[] { 7, 8 });
        //Initlaser(lasers);
        ActivateTheLasers(maxWidth1, lasers);
        yield return new WaitForSeconds(2f);
        ActivateTheLasers(maxWidth2, lasers);
        yield return new WaitForSeconds(2f);
        DeActivateTheLasers(lasers);
        //Finishedlaser(lasers);

        lasers = laserRoom.GetLaserTurrets(new int[] { 0, 16 });
        //Initlaser(lasers);
        ActivateTheLasers(maxWidth1, lasers);
        yield return new WaitForSeconds(2f);
        ActivateTheLasers(maxWidth2, lasers);
        yield return new WaitForSeconds(2f);
        DeActivateTheLasers(lasers);
        //Finishedlaser(lasers);

        lasers = laserRoom.GetLaserTurrets(new int[] { 6, 12 });
        //Initlaser(lasers);
        ActivateTheLasers(maxWidth1, lasers);
        yield return new WaitForSeconds(2f);
        ActivateTheLasers(maxWidth2, lasers);
        yield return new WaitForSeconds(2f);
        DeActivateTheLasers(lasers);
        //Finishedlaser(lasers);

        lasers = laserRoom.GetLaserTurrets(new int[] { 6, 12 }, new int[] { 0, 2 });
        //Initlaser(lasers);
        ActivateTheLasers(maxWidth1, lasers);
        yield return new WaitForSeconds(2f);
        ActivateTheLasers(maxWidth2, lasers);
        yield return new WaitForSeconds(2f);
        DeActivateTheLasers(lasers);
        //Finishedlaser(lasers);

        lasers = laserRoom.GetLaserTurrets(new int[] { 6, 12 }, new int[] { 1, 3 });
        //Initlaser(lasers);
        ActivateTheLasers(maxWidth1, lasers);
        yield return new WaitForSeconds(2f);
        ActivateTheLasers(maxWidth2, lasers);
        yield return new WaitForSeconds(2f);
        DeActivateTheLasers(lasers);
        //Finishedlaser(lasers);

        lasers = laserRoom.GetLaserTurrets(new int[] { 6, 12 }, new int[] { 0, 2 });
        //Initlaser(lasers);
        ActivateTheLasers(maxWidth1, lasers);
        yield return new WaitForSeconds(2f);
        ActivateTheLasers(maxWidth2, lasers);
        yield return new WaitForSeconds(2f);
        DeActivateTheLasers(lasers);
        Finishedlaser(lasers);
        canTransition = true;
        theBoss.laserRoomFinished = true;
        yield break;
    }

    public void Finishedlaser(List<StationaryLaser> stationaryLasers)
    {
        foreach (StationaryLaser stationaryLaser in stationaryLasers)
        {
            stationaryLaser.Line.enabled = false;
            stationaryLaser.Box.enabled = false;
        }

    }

    public void Initlaser(List<StationaryLaser> stationaryLasers)
    {
        foreach (StationaryLaser stationaryLaser in stationaryLasers)
        {
            stationaryLaser.Line.enabled = true;
            stationaryLaser.Box.enabled = true;
        }
    }

    public void ActivateTheLasers(float maxsize, List<StationaryLaser> stationaryLasers)
    {
        foreach(StationaryLaser stationaryLaser in stationaryLasers)
        {
            stationaryLaser.Line.enabled = true;
            stationaryLaser.Box.enabled = true;
            stationaryLaser.IncreaseLaserSize(maxsize);
        }
    }
    public void DeActivateTheLasers(List<StationaryLaser> stationaryLasers)
    {
        foreach (StationaryLaser stationaryLaser in stationaryLasers)
        {
            stationaryLaser.DecreaseLaserSize();
            stationaryLaser.Line.enabled = false;
            stationaryLaser.Box.enabled = false;
        }
    }
}
