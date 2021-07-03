using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public abstract class IState
{
    public bool canTransition;
    protected TheBoss theBoss;
    protected LaserRoom laserRoom;

    protected void RotateTowardsPlayer()
    {
        if (!theBoss.isOnCorner1)
        {
            theBoss.targetRotation.LookAt(theBoss.corner1.transform.position);
            theBoss.transform.rotation = Quaternion.Slerp(theBoss.transform.rotation, theBoss.targetRotation.rotation, 4 * Time.deltaTime);
        }else
        {
            theBoss.targetRotation.LookAt(theBoss.corner2.transform.position);
            theBoss.transform.rotation = Quaternion.Slerp(theBoss.transform.rotation, theBoss.targetRotation.rotation, 4 * Time.deltaTime);
        }
    }
    protected void SetTargetRotation()
    {
        if (!theBoss.isOnCorner1)
        {
            theBoss.targetRotation.LookAt(theBoss.corner1.transform.position);
        }
        else
        {
            theBoss.targetRotation.LookAt(theBoss.corner2.transform.position);
        }
    }
    public virtual IEnumerator Enter()
    {
        yield break;
    }
}
