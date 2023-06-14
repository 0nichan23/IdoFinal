using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSheet : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.Instance.PlayerWrapper.UpdatePlayerHud();
    }
}
