using System.Collections.Generic;
using UnityEngine;

public class PlayerTeam : MonoBehaviour
{
    private Animal activeAnimal;
    private List<Animal> backLineAnimals = new List<Animal>();
    public void SetActiveAnimal(Animal givenAnimal)
    {
        activeAnimal = givenAnimal;
    }



}


