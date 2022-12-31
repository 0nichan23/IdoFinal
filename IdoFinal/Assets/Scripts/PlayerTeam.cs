using System.Collections.Generic;
using UnityEngine;

public class PlayerTeam : MonoBehaviour
{
    private Animal activeAnimal;
    private List<Animal> backLineAnimals = new List<Animal>();

    [SerializeField] private DamageDealer damageDealer;

    public DamageDealer DamageDealer { get => damageDealer; }

    public void SetActiveAnimal(Animal givenAnimal)
    {
        activeAnimal = givenAnimal;
    }

    private void SubscirbeAnimalPassives()
    {
        foreach (var item in backLineAnimals)
        {
            item.Passive.SubscribePassive(this);
        }
    }

}


