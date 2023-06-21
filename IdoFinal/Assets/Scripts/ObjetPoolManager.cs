using UnityEngine;

public class ObjetPoolManager : MonoBehaviour
{
    [SerializeField] private ProjectilePooler lightningProjectilePool;
    [SerializeField] private ProjectilePooler fireProjectilePool;
    [SerializeField] private ProjectilePooler poisonProjectilePool;
    [SerializeField] private ProjectilePooler iceProjectilePool;
    [SerializeField] private ExplosionPool lightningBlastPool;
    [SerializeField] private ExplosionPool fireBlastPool;
    [SerializeField] private ExplosionPool poisonBlastPool;
    [SerializeField] private ExplosionPool iceBlastPool;
    [SerializeField] private DropPool dropPool;

    public ProjectilePooler LightningProjectilePool { get => lightningProjectilePool; }
    public ProjectilePooler FireProjectilePool { get => fireProjectilePool; }
    public ProjectilePooler PoisonProjectilePool { get => poisonProjectilePool; }
    public ProjectilePooler IceProjectilePool { get => iceProjectilePool; }
    public ExplosionPool LightningBlastPool { get => lightningBlastPool; }
    public ExplosionPool FireBlastPool { get => fireBlastPool; }
    public ExplosionPool PoisonBlastPool { get => poisonBlastPool; }
    public ExplosionPool IceBlastPool { get => iceBlastPool; }
    public DropPool DropPool { get => dropPool; }
}
