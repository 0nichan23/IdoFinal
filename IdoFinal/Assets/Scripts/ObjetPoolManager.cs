using UnityEngine;

public class ObjetPoolManager : MonoBehaviour
{
    [SerializeField] private ProjectilePooler testProjectilePool;

    public ProjectilePooler TestProjectilePool { get => testProjectilePool; }
}
