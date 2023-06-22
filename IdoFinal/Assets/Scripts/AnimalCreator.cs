using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalCreator
{
    public Animal CraftAnimal(FoodDropSO food, BiomeDropSO biomeDrop)
    {
        List<Animal> validAnimals = new List<Animal>();
        List<Animal> availableAnimals = new List<Animal>();
        foreach (var item in GameManager.Instance.LevelGenerator.BiomeData)
        {
            if (biomeDrop.Habitat == item.HeightData.Habitat)
            {
                foreach (var animalSpawnData in item.HeightData.EnemyCreator.SpawnData)
                {
                    if (GameManager.Instance.PlayerWrapper.Team.CheckAnimalAvailable(animalSpawnData.AnimalToSpawn))
                    {
                        availableAnimals.Add(animalSpawnData.AnimalToSpawn);
                        if (animalSpawnData.AnimalToSpawn.Rarity == biomeDrop.Rarity && animalSpawnData.AnimalToSpawn.Diet == food.Diet)
                        {
                            validAnimals.Add(animalSpawnData.AnimalToSpawn);
                        }
                    }
                }
                if (validAnimals.Count > 0)//return a random valid animal as long as at least 1 was found
                {
                    return validAnimals[Random.Range(0, validAnimals.Count)];
                }
                else if (availableAnimals.Count > 0)
                {
                    return availableAnimals[Random.Range(0, availableAnimals.Count)];
                }
                else
                {
                    return null;
                }
            }
        }
        return null;
    }

}
