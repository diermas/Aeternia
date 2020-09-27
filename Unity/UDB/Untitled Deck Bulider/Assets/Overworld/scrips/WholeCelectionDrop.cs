using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WholeCelectionDrop : MonoBehaviour, IDropHandler
{
    public string activesupplyPlayer;
    public void OnDrop(PointerEventData eventData)
    {
        DragAndDrop.card.transform.SetParent(transform);
        switch (activesupplyPlayer)
        {
            case "Rouge":
                PlayerCreation.playerRogue.OwnedCards.Add(int.Parse(DragAndDrop.card.name));
                PlayerCreation.playerRogue.activeSupply.Remove(int.Parse(DragAndDrop.card.name));

                break;
            case "Warior":
                PlayerCreation.playerWarrior.OwnedCards.Add(int.Parse(DragAndDrop.card.name));
                PlayerCreation.playerWarrior.activeSupply.Remove(int.Parse(DragAndDrop.card.name));
                break;
            case "Mage":
                PlayerCreation.playerMage.OwnedCards.Add(int.Parse(DragAndDrop.card.name));
                PlayerCreation.playerMage.activeSupply.Remove(int.Parse(DragAndDrop.card.name));
                break;
            case "Preast":
                PlayerCreation.playerPriest.OwnedCards.Add(int.Parse(DragAndDrop.card.name));
                PlayerCreation.playerPriest.activeSupply.Remove(int.Parse(DragAndDrop.card.name));
                break;
        }
    }
}
