using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRoom : MonoBehaviour
{
    List<StationaryLaser> stationaryLasersVertical = new List<StationaryLaser>();
    List<StationaryLaser> stationaryLasersHorizontal = new List<StationaryLaser>();

    private void Awake()
    {
        GetComponentsInChildren<StationaryLaser>(stationaryLasersVertical);
        foreach(StationaryLaser laser in stationaryLasersVertical)
        {
            if (laser.IsHorizontal)
            {
                stationaryLasersHorizontal.Add(laser);             
            }
        }
        stationaryLasersVertical.RemoveAll(laser => laser.IsHorizontal);
    }

    public List<StationaryLaser> GetLaserTurrets(int[] ints)
    {
        List<StationaryLaser> stationaryLasersToActivate = new List<StationaryLaser>();
        List<int> lasersToIgnore = new List<int>();
        lasersToIgnore.AddRange(ints);
        for (int i = 0; i < stationaryLasersVertical.Count; i++) // two random turrets get taken out of the ToBeActivated list
        {
            if(!lasersToIgnore.Contains(i))
            {
                stationaryLasersToActivate.Add(stationaryLasersVertical[i]);
            }
        }
        return stationaryLasersToActivate;
    }
    public List<StationaryLaser> GetLaserTurrets(int[] ints, int[] ints2)
    {
        List<StationaryLaser> stationaryLasersToActivate = new List<StationaryLaser>();
        List<int> lasersToIgnore = new List<int>();
        lasersToIgnore.AddRange(ints);
        for (int i = 0; i < stationaryLasersVertical.Count; i++) // two random turrets get taken out of the ToBeActivated list
        {
            if (!lasersToIgnore.Contains(i))
            {
                stationaryLasersToActivate.Add(stationaryLasersVertical[i]);
            }
        }
        lasersToIgnore.Clear();
        lasersToIgnore.AddRange(ints2);
        for (int i = 0; i < stationaryLasersHorizontal.Count; i++) // x random turrets get taken out of the ToBeActivated list
        {
            if (!lasersToIgnore.Contains(i))
            {
                stationaryLasersToActivate.Add(stationaryLasersHorizontal[i]);
            }
        }
        return stationaryLasersToActivate;
    }
}
