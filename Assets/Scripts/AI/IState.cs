using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IState 
{
    public bool canTransition;
    protected TheBoss theBoss;

    protected void RotateTowardsPlayer()
    {
        theBoss.targetRotation.LookAt(theBoss.player.transform);
        theBoss.transform.rotation = Quaternion.Slerp(theBoss.transform.rotation, theBoss.targetRotation.rotation, 4 * Time.deltaTime);
    }
    public virtual IEnumerator Enter()
    {
        yield break;
    }
}
