using UnityEngine;
using UnityEngine.UI;

public class AnimalInventorySlot : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Button button;
    private Animal refAnimal;

    public Image Image { get => image; }
    public Animal RefAnimal { get => refAnimal; }
    public Button Button { get => button; }

    public void SetUp(Animal givenAnimal)
    {
        refAnimal = givenAnimal;
        image.sprite = refAnimal.Portrait;
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
    }

    public void Clear()
    {
        refAnimal = null;
        image.sprite = null;
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
    }
    public void SelectSlot()
    {
        GameManager.Instance.PlayerWrapper.PlayerHud.TeamPanel.SelectedPanel.SelectSlot(this);
    }
   
    public void UnselectSlot()
    {
        Clear();
    }

}
