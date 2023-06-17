using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SwitchAttackIcon : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private float coolDown;
    public UnityEvent AttackSwitchUp;

    public void StartCountDown()
    {
        slider.maxValue = coolDown;
        slider.value = slider.maxValue;
        StartCoroutine(CountDown());
    }

    private IEnumerator CountDown()
    {
        while (slider.value > 0)
        {
            slider.value -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        slider.value = 0;
        AttackSwitchUp?.Invoke();
    }
}
