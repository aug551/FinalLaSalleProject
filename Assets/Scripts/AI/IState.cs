using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IState 
{
    public bool canTransition;
    public virtual IEnumerator Enter(TheBoss theBoss)
    {
        yield break;
    }
    public virtual IEnumerator Exit()
    {
        yield break;
    }
}
