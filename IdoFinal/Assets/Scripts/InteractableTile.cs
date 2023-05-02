using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using System;

public class InteractableTile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer rend;
    [SerializeField] Color damageColor;
    [SerializeField, Range(1,5)] float damageLerpTimeMod = 1;
    Color startColor;

    public void DamageColor()
    {
        startColor = rend.color;
        StartCoroutine(DamageColorChange());
    }

    IEnumerator DamageColorChange()
    {
        float counter = 0;
        while (counter < 1)
        {
            counter += Time.deltaTime * damageLerpTimeMod;
            rend.color = Color.Lerp(startColor, damageColor, counter);
            yield return new WaitForEndOfFrame();
        }
        rend.color = startColor;
        gameObject.SetActive(false);
    }
}
