using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEyes : MonoBehaviour
{
    RaycastHit hit;
    public void activateTheLasers(TheBoss theBoss)
    {
        if (Physics.Raycast(theBoss.eyepos.position, transform.forward, out hit))
        {
            theBoss.LineRenderer.SetPosition(1, hit.point);
        }
    }
}
