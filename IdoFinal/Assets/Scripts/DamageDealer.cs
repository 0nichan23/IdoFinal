using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageDealer : MonoBehaviour
{
    public UnityEvent<AnimalAttack> OnDealDamage;
    public UnityEvent<AnimalAttack> OnDealDamageFinal;

    public UnityEvent<StatusEffect> OnApplyStatus;

    public UnityEvent OnKill;
}
