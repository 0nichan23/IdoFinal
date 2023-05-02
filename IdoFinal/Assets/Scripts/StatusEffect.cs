using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffect
{
    
    public virtual void Activate()
    {
        Subscribe();
    }

    public virtual void Remove()
    {
        UnSubscribe();
    }

    public virtual void Reset()
    {

    }

    protected virtual void Subscribe()
    {

    }
    protected virtual void UnSubscribe()
    {

    }


}
   

