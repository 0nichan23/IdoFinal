using UnityEngine;

public abstract class AnimalPassive : ScriptableObject
{
    [SerializeField] protected string effect;

    public string Effect { get => effect; }

    public abstract void SubscribePassive(Character givenCaharacter);

    public abstract void UnSubscribePassive(Character givenCaharacter);

}
