using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamPanel : MonoBehaviour
{
    //ui handler for everything regarding the team ui panel- easy access through player hud
    [SerializeField] private Button confirm;
    [SerializeField] private Button close;
    [SerializeField] private SelectedAnimalsPanel selectedPanel;
    [SerializeField] private AvailableAnimalPanel inventoryPanel;

    public SelectedAnimalsPanel SelectedPanel { get => selectedPanel; }
    public AvailableAnimalPanel InventoryPanel { get => inventoryPanel; }

    public void ConfirmTeam()
    {
        List<Animal> newTeam = new List<Animal>();
        foreach (var item in selectedPanel.SelectedSlots)
        {
            if (ReferenceEquals(item.RefAnimal, null))
            {
                return;
            }
            newTeam.Add(item.RefAnimal);
        }
        GameManager.Instance.PlayerWrapper.Team.SetNewTeam(newTeam);
        ToggleTeamPanel();
    }

    public void ToggleTeamPanel()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

}
