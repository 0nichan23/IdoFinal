using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnimalCard : MonoBehaviour
{
    [SerializeField] private Image animalImage;
    [SerializeField] private TextMeshProUGUI aniamlName;
    [SerializeField] private Slider attack;
    [SerializeField] private Slider def;
    [SerializeField] private Slider inst;
    [SerializeField] private Slider spd;
    private Animal refAnimal;

    public void SetUpCard(Animal givenAnimal)
    {
        refAnimal = givenAnimal;
        aniamlName.text = refAnimal.name;
        attack.maxValue = 10;
        def.maxValue = 10;
        inst.maxValue = 10;
        spd.maxValue = 10;
        attack.value = refAnimal.StatSheet.Power;
        def.value = refAnimal.StatSheet.Defense;
        inst.value = refAnimal.StatSheet.Instinct;
        spd.value = refAnimal.StatSheet.Speed;
    }
}
