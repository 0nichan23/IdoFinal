using UnityEngine;

public class AnimalDropHandler : MonoBehaviour
{
    [SerializeField] private DropSO largeMeat;
    [SerializeField] private DropSO mediumMeat;
    [SerializeField] private DropSO smallMeat;
    [SerializeField] private DropSO largeLeaf;
    [SerializeField] private DropSO mediumLeaf;
    [SerializeField] private DropSO smallLeaf;
    public void Drop(Character character)
    {
        switch (character.Damageable.RefAnimal.Diet)
        {
            case Diet.Carnivore:
                switch (character.Damageable.RefAnimal.Size)
                {
                    case Size.Small:
                        GetAndSetupDrop(smallMeat, character);
                        break;
                    case Size.Medium:
                        GetAndSetupDrop(mediumMeat, character);
                        break;
                    case Size.Large:
                        GetAndSetupDrop(largeMeat, character);
                        break;
                }
                break;
            case Diet.Herbivore:
                switch (character.Damageable.RefAnimal.Size)
                {
                    case Size.Small:
                        GetAndSetupDrop(smallLeaf, character);
                        break;
                    case Size.Medium:
                        GetAndSetupDrop(mediumLeaf, character);
                        break;
                    case Size.Large:
                        GetAndSetupDrop(largeLeaf, character);
                        break;
                }
                break;
            case Diet.Omnivore:
                float rnd = Random.Range(0, 100);
                switch (character.Damageable.RefAnimal.Size)
                {
                    case Size.Small:
                        if (rnd <= 50)
                        {
                            GetAndSetupDrop(smallMeat, character);
                        }
                        else
                        {
                            GetAndSetupDrop(smallLeaf, character);
                        }
                        break;
                    case Size.Medium:
                        if (rnd <= 50)
                        {
                            GetAndSetupDrop(mediumMeat, character);
                        }
                        else
                        {
                            GetAndSetupDrop(mediumLeaf, character);
                        }
                        break;
                    case Size.Large:
                        if (rnd <= 50)
                        {
                            GetAndSetupDrop(largeMeat, character);
                        }
                        else
                        {
                            GetAndSetupDrop(largeLeaf, character);
                        }
                        break;
                }
                break;
            default:
                break;
        }

    }


    private void GetAndSetupDrop(DropSO food, Character character)
    {
        Drop drop = GameManager.Instance.PoolManager.DropPool.GetPooledObject();
        drop.SetUp(food.Artwork);
        drop.transform.position = character.transform.position;
        drop.gameObject.SetActive(true);
    }
}
