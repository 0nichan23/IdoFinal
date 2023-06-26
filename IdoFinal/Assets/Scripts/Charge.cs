using UnityEngine;

[CreateAssetMenu(fileName = "New Charge", menuName = "Charge")]

public class Charge : AnimalAttack
{
    [SerializeField] private Element blast;

    public Element Blast { get => blast; }
}
