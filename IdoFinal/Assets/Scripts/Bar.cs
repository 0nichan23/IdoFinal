using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Bar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI animalName;

    public void SetUp(Animal animal)
    {
        animalName.text = animal.name;
    }
    public void UpdateBar(float maxHp, float curHp)
    {
        slider.maxValue = maxHp;
        slider.value = curHp;
        text.text = Mathf.RoundToInt(curHp).ToString() + "/" + Mathf.RoundToInt(maxHp).ToString();
    }
}
