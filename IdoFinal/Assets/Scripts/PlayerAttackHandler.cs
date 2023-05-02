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

    private float attackSpeedMod;
    private float baseAttackSpeedMod;

    public AnimalAttack CurrentAttack { get => currentAttack; }

    public void SetStats(Animal givenActiveAnimal)
    {
        baseAttackSpeedMod = GetAttackSpeedModBase(givenActiveAnimal.StatSheet.Speed);
        EquipAttack(givenActiveAnimal.Attack);
    }

    private float GetAttackSpeedModBase(int baseSpeed)
    {
        //10 speed = 100% faster attack speed/ half cool down
        float speedMod = 0f;
        for (int i = 0; i < baseSpeed; i++)
        {
            speedMod += 0.05f;
        }
        return speedMod;
    }

    public void AddAttackSpeed(float givenMod)
    {
        attackSpeedMod += givenMod;
    }

    public float GetAttackCoolDown()
    {
        float cd = currentAttack.CoolDown;
        cd -= cd * (baseAttackSpeedMod + attackSpeedMod);
        return cd;
    }

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
        if (Time.time - lastAttacked < GetAttackCoolDown())
        {
            return;
        }
        lastAttacked = Time.time;
        OnAttackPreformed?.Invoke();
        targeter.AttackTiles(GameManager.Instance.PlayerWrapper.PlayerMovement.CurrentTile.GetPos, currentAttack, dealer);
    }


    public void EquipAttack(AnimalAttack givenAttack)
    {
        currentAttack = givenAttack;
        lastAttacked = GetAttackCoolDown() * -1;
        OnAttackSwitched?.Invoke();
    }
}
