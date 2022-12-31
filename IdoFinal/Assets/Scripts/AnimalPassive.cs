using UnityEngine;

public abstract class AnimalPassive : ScriptableObject
{
    //subscribe to the playerteam's correct event
    //
    //on damage, stat boost, on death, on take damage, on beat level?, on lose level?

    //synergy passives -> if 3 or more animals are from the same habitat
    protected Animal host;
    public virtual void SubscribePassive()
    {

    }
    public virtual void UnSubscribePassive()
    {

    }

    public void CacheHost(Animal givenAnimal)
    {
        host = givenAnimal;
        SubscribePassive();    
    }
}
