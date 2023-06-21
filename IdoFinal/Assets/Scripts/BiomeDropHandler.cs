using UnityEngine;


[CreateAssetMenu(fileName = "BiomeDropper", menuName = "BiomeDropper")]
public class BiomeDropHandler : ScriptableObject
{
    [SerializeField] private BiomeDropSO commonDrop;
    [SerializeField] private BiomeDropSO unCommonDrop;
    [SerializeField] private BiomeDropSO rareDrop;
    public void Drop(Character character)
    {
        switch (character.Damageable.RefAnimal.Rarity)
        {
            case Rarity.Common:
                GetAndSetupDrop(commonDrop, character);
                break;
            case Rarity.Uncommon:
                GetAndSetupDrop(unCommonDrop, character);
                break;
            case Rarity.Rare:
                GetAndSetupDrop(rareDrop, character);
                break;
            case Rarity.Boss:
                //boss drop that is universal
                break;
        }

    }

    private void GetAndSetupDrop(BiomeDropSO biomeDrop, Character character)
    {
        Drop drop = GameManager.Instance.PoolManager.DropPool.GetPooledObject();
        drop.SetUp(biomeDrop.Artwork);
        drop.transform.position = character.transform.position;
        drop.gameObject.SetActive(true);
        GameManager.Instance.PlayerWrapper.DropsInventory.AddDrop(biomeDrop);
    }
}
