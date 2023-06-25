using System.Collections.Generic;

public class GameData
{
    public int playerLevel;
    public List<Animal> caughtAnimals;

    public GameData()
    {
        playerLevel = 1;
        caughtAnimals = new List<Animal>(GameManager.Instance.PlayerWrapper.AnimalInventory.DefaultList);
    }
}
