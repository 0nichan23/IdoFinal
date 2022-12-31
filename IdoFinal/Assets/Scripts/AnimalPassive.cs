using UnityEngine;

public abstract class AnimalPassive : ScriptableObject
{
    //subscribe to the playerteam's correct event
    //
    //on damage, stat boost, on death, on take damage, on beat level?, on lose level?

    //synergy passives -> if 3 or more animals are from the same habitat

    public virtual void SubscribePassive(PlayerTeam givenTeam)
    {
        //sub to event
    }
    public virtual void UnSubscribePassive(PlayerTeam givenTeam)
    {
        //unsub from event
    }
}
