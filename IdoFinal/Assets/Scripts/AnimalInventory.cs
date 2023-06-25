using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimalInventory : MonoBehaviour, ISaveable
{
    //we add animals here 
    //all caught animals will be stored here

    [SerializeField] private List<Animal> caughtAnimals = new List<Animal>();
    [SerializeField] private List<Animal> defaultList = new List<Animal>();

    public UnityEvent<Animal> OnAnimalAdded;
    public List<Animal> CaughtAnimals { get => caughtAnimals; }
    public List<Animal> DefaultList { get => defaultList; }

    public void AddAnimal(Animal givenAnimal)
    {
        if (caughtAnimals.Contains(givenAnimal))
        {
            return;
        }
        caughtAnimals.Add(givenAnimal);
        OnAnimalAdded?.Invoke(givenAnimal);
    }

    public void LoadData(GameData data)
    {
        caughtAnimals = new List<Animal>(data.caughtAnimals);
    }

    public void SaveGame(ref GameData data)
    {
        data.caughtAnimals = new List<Animal>(caughtAnimals);
    }
}
