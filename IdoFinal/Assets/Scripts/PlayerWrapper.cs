using UnityEngine;

public class PlayerWrapper : Character
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerAnimationHandler playerAnimationHandler;
    [SerializeField] private Transform gfx;
    [SerializeField] private PlayerTeam team;
    [SerializeField] private PlayerAttackHandler attackHandler;

    public PlayerMovement PlayerMovement { get => playerMovement; }
    public PlayerAnimationHandler PlayerAnimationHandler { get => playerAnimationHandler; }
    public Transform Gfx { get => gfx; }
    public PlayerTeam Team { get => team; }

    private void Start()
    {
        SetAnimalStatsOnComps();
        team.OnSwitchActiveAnimal.AddListener(SetAnimalStatsOnComps);
        attackHandler.OnAttackPreformed.AddListener(playerAnimationHandler.AttackAnim);
        GameManager.Instance.LevemManager.CurrentLevel.OnDoneCreatingRoom.AddListener(playerMovement.ResetCanMove);
        attackHandler.CacheDealer(DamageDealer);
        attackHandler.EquipAttack(team.ActiveAnimal.Attack);
        Damageable.CacheEffectable(Effectable);
    }

    private void SetAnimalStatsOnComps()
    {
        Damageable.SetStats(team.ActiveAnimal);
        Damageable.Heal(new DamageHandler() { BaseAmount = Damageable.MaxHp });
        attackHandler.EquipAttack(team.ActiveAnimal.Attack);
        DamageDealer.SetStats(team.ActiveAnimal);
    }



    //items 
    //oncrits remove mods
    //reset crits afer subscribing trinkets
}
