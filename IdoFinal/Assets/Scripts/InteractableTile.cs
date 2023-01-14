using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using System;

public class InteractableTile : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private TileData refTile;
    [SerializeField] private SpriteRenderer border;
    Color startColor;
    public TileData RefTile { get => refTile; }

    public void CacheRefTile(TileData givenTile)
    {
        refTile = givenTile;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("clicked " + gameObject.name);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        startColor = border.color;
        border.color = Color.red;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        border.color = startColor;

    }
}
