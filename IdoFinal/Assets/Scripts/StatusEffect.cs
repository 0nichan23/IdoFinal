using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffect
{
    protected Character host;
    protected EffectOrientation effectOri;

    public EffectOrientation EffectOri { get => effectOri; }

    public void CacheHost(Character givenCharacter)
    {
        host = givenCharacter;
    }

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

public enum EffectOrientation
{
    POS,
    NEG
}
   

