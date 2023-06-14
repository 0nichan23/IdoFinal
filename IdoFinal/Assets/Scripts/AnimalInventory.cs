using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimalInventory : MonoBehaviour
{
    //we add animals here 
    //all caught animals will be stored here

    [SerializeField] private List<Animal> caughtAnimals = new List<Animal>();

    public UnityEvent<Animal> OnAnimalAdded;
    public List<Animal> CaughtAnimals { get => caughtAnimals; }


    public void AddAnimal(Animal givenAnimal)
    {
        if (caughtAnimals.Contains(givenAnimal))
        {
            return;
        }
        caughtAnimals.Add(givenAnimal);
        OnAnimalAdded?.Invoke(givenAnimal);
    }


}
