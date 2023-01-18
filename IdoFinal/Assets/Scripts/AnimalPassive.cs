using UnityEngine;

public abstract class AnimalPassive : ScriptableObject
{
    public abstract void SubscribePassive(Character givenCaharacter);

    public abstract void UnSubscribePassive(Character givenCaharacter);

}
