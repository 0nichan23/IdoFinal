using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAttackHandler : MonoBehaviour
{
    public UnityEvent OnAttackPreformed;
    public UnityEvent OnAttackSwitched;
    private AnimalAttack currentAttack;
    private float lastAttacked;
    private DamageDealer dealer;
    private AttackTarget targeter = new AttackTarget();

    private void Start()
    {
        GameManager.Instance.InputManager.OnAttack.AddListener(Attack);
    }


    public void CacheDealer(DamageDealer givenDealer)
    {
        dealer = givenDealer;
    }

    private void Attack()
    {
        if (Time.time - lastAttacked < currentAttack.CoolDown)
        {
            return;
        }
        lastAttacked = Time.time;
        OnAttackPreformed?.Invoke();
        targeter.AttackTiles(GameManager.Instance.PlayerWrapper.PlayerMovement.CurrentTile.GetPos ,currentAttack, dealer);
    }




    public void EquipAttack(AnimalAttack givenAttack)
    {
        currentAttack = givenAttack;
        lastAttacked = currentAttack.CoolDown * -1;
        OnAttackSwitched?.Invoke();
    }
}
