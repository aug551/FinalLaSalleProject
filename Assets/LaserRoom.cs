using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRoom : MonoBehaviour
{
    List<StationaryLaser> stationaryLasers = new List<StationaryLaser>();

    private void Awake()
    {
        GetComponentsInChildren<StationaryLaser>(stationaryLasers);
    }

    public List<StationaryLaser> GetLaserTurrets(int[] ints)
    {
        List<StationaryLaser> stationaryLasersToActivate = new List<StationaryLaser>();
        List<int> lasersToIgnore = new List<int>();
        lasersToIgnore.AddRange(ints);
        for (int i = 0; i < stationaryLasers.Count; i++) // two random turrets get taken out of the ToBeActivated list
        {
            if(!lasersToIgnore.Contains(i))
            {
                stationaryLasersToActivate.Add(stationaryLasers[i]);
            }
        }
        return stationaryLasersToActivate;
    }
}
