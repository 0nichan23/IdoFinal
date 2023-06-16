using UnityEngine;
using UnityEngine.Events;

public class PlayerAttackHandler : MonoBehaviour
{
    public UnityEvent OnAttackPreformed;
    public UnityEvent OnAttackSwitched;
    private AnimalAttack currentAttack;
    private AnimalAttack currentSecondAttack;
    private float lastAttacked;
    private DamageDealer dealer;
    private AttackTarget targeter = new AttackTarget();
    private AttackCounter attackCounter = new AttackCounter();
    private bool canAttack;

    private float attackSpeedMod;
    private float baseAttackSpeedMod;

    public float AttackSpeed { get => Mathf.Clamp(baseAttackSpeedMod + attackSpeedMod, 0f, 0.9f); }

    public AnimalAttack CurrentAttack { get => currentAttack; }
    public AttackCounter AttackCounter { get => attackCounter; }
    public bool CanAttack { get => canAttack; set => canAttack = value; }
    public AnimalAttack CurrentSecondAttack { get => currentSecondAttack; }

    public void SetStats(Animal givenActiveAnimal)
    {
        baseAttackSpeedMod = GetAttackSpeedModBase(givenActiveAnimal.StatSheet.Speed);
        EquipAttack(givenActiveAnimal.Attack);
        if (!ReferenceEquals(givenActiveAnimal.SecondAttack, null))
        {
            currentSecondAttack = givenActiveAnimal.SecondAttack;
        }
        else
        {
            currentSecondAttack = null;
        }
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
        cd -= cd * AttackSpeed;
        return cd;
    }

    private void Start()
    {
        GameManager.Instance.InputManager.OnAttack.AddListener(Attack);
        GameManager.Instance.InputManager.OnSwitchAttacks.AddListener(SwitchAttacks);
        OnAttackPreformed.AddListener(attackCounter.CountAttacks);
    }


    public void CacheDealer(DamageDealer givenDealer)
    {
        dealer = givenDealer;
    }

    private void Attack()
    {
        if (Time.time - lastAttacked < GetAttackCoolDown() || !canAttack)
        {
            return;
        }
        lastAttacked = Time.time;
        OnAttackPreformed?.Invoke();
        if (currentAttack.Projectile)
        {
            GameManager.Instance.PlayerWrapper.FireProjectile(currentAttack);
        }
        else
        {
            targeter.AttackTiles(GameManager.Instance.PlayerWrapper.LookingTowards, GameManager.Instance.PlayerWrapper.PlayerMovement.CurrentTile.GetPos, currentAttack, dealer);
        }
    }


    public void SwitchAttacks()//switches first and second attack
    {
        if (ReferenceEquals(currentSecondAttack, null))
        {
            return;
        }
        AnimalAttack temp = currentAttack;
        EquipAttack(currentSecondAttack);
        currentSecondAttack = temp;
    }

    public void EquipAttack(AnimalAttack givenAttack)
    {
        currentAttack = givenAttack;
        lastAttacked = GetAttackCoolDown() * -1;
        OnAttackSwitched?.Invoke();
    }


}
