using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DropIcon : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Button button;
    private BasicDropSO refDrop;
    public Image Image { get => image; }
    public BasicDropSO RefDrop { get => refDrop; }

    public void SetUp(BasicDropSO givenDrop, Sprite sprite, int amount, UnityAction onclick)
    {
        refDrop = givenDrop;
        image.sprite = sprite;
        text.text = amount.ToString();
        button.onClick.AddListener(onclick);
    }

    public void SelectFood()
    {
        GameManager.Instance.PlayerWrapper.PlayerHud.DropPanel.SelectFoodIcon(this);
    }

    public void SelectBiomeDrop()
    {
        GameManager.Instance.PlayerWrapper.PlayerHud.DropPanel.SelectBiomeDropIcon(this);
    }
}
