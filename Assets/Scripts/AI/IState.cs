using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IState 
{
    public bool canTransition;
    protected TheBoss theBoss;
    public virtual IEnumerator Enter()
    {
        yield break;
    }
    public virtual IEnumerator Exit()
    {
        yield break;
    }
}
