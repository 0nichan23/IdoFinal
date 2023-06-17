using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackIcon : MonoBehaviour
{
    [SerializeField] private Image attackIcon;
    [SerializeField] private Slider slider;
    //get the total calculated cd of the attack after attacking and count backwards
    public void SetUpAttack(float coolDown)
    {
        slider.maxValue = coolDown;
        slider.value = slider.maxValue;
    }

    public void SetNewAttack(Sprite givenSprite)
    {
        attackIcon.sprite = givenSprite;
    }

    private void Update()
    {
        if (slider.value > 0)
        {
            slider.value -= Time.deltaTime;
        }
    }


}
