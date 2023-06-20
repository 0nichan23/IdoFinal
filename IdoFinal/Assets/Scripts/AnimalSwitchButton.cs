using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AnimalSwitchButton : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Slider coolDownSlider;
    [SerializeField] private float coolDownDuration;
    private Animal refAnimal;
    private bool canSwitch;

    private void Start()
    {
        coolDownSlider.maxValue = coolDownDuration;
        canSwitch = true;
    }
    public void CacheAnimal(Animal givenAnimal)
    {
        refAnimal = givenAnimal;
        image.sprite = refAnimal.Portrait;
    }

    public void StartCoolDown()
    {
        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        canSwitch = false;
        coolDownSlider.value = coolDownSlider.maxValue;
        while (coolDownSlider.value > 0)
        {
            coolDownSlider.value -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        coolDownSlider.value = 0;
        canSwitch = true;
    }

    public void SelectAnimal(int index)
    {
        if (canSwitch)
        {
            GameManager.Instance.PlayerWrapper.Team.SwitchActiveAnimal(index);
        }
    }
}
