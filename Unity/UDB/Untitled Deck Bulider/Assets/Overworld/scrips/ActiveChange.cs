using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActiveChange : MonoBehaviour, IDropHandler
{
    public string activesupplyPlayer;
    public GameObject DragCard
    {
        get
        {
            if(transform.childCount>0)
            {
                return transform.GetChild(0).gameObject;
            }
            return null;
        }
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (!DragCard)
        {
            DragAndDrop.card.transform.SetParent(transform);
            switch (activesupplyPlayer)
            {
                case "Rouge":
                    PlayerCreation.playerRogue.activeSupply.Add(int.Parse(DragAndDrop.card.name));
                    PlayerCreation.playerRogue.OwnedCards.Remove(int.Parse(DragAndDrop.card.name));
                    break;
                case "Warior":
                    PlayerCreation.playerWarrior.activeSupply.Add(int.Parse(DragAndDrop.card.name));
                    PlayerCreation.playerWarrior.OwnedCards.Remove(int.Parse(DragAndDrop.card.name));
                    break;
                case "Mage":
                    PlayerCreation.playerMage.activeSupply.Add(int.Parse(DragAndDrop.card.name));
                    PlayerCreation.playerMage.OwnedCards.Remove(int.Parse(DragAndDrop.card.name));

                    break;
                case "Preast":
                    PlayerCreation.playerPriest.activeSupply.Add(int.Parse(DragAndDrop.card.name));
                    PlayerCreation.playerPriest.OwnedCards.Remove(int.Parse(DragAndDrop.card.name));

                    break;
            }
        }
        else
        {
            switch (activesupplyPlayer)
            {
                case "Rouge":
                    PlayerCreation.playerRogue.activeSupply.Add(int.Parse(DragAndDrop.card.name));
                    PlayerCreation.playerRogue.activeSupply.Remove(int.Parse(DragCard.name));
                    PlayerCreation.playerRogue.OwnedCards.Remove(int.Parse(DragAndDrop.card.name));
                    PlayerCreation.playerRogue.OwnedCards.Add(int.Parse(DragCard.name));
                    break;
                case "Warior":
                    PlayerCreation.playerWarrior.activeSupply.Add(int.Parse(DragAndDrop.card.name));
                    PlayerCreation.playerWarrior.activeSupply.Remove(int.Parse(DragCard.name));
                    PlayerCreation.playerWarrior.OwnedCards.Remove(int.Parse(DragAndDrop.card.name));
                    PlayerCreation.playerWarrior.OwnedCards.Add(int.Parse(DragCard.name));
                    break;
                case "Mage":
                    PlayerCreation.playerMage.activeSupply.Add(int.Parse(DragAndDrop.card.name));
                    PlayerCreation.playerMage.activeSupply.Remove(int.Parse(DragCard.name));
                    PlayerCreation.playerMage.OwnedCards.Remove(int.Parse(DragAndDrop.card.name));
                    PlayerCreation.playerMage.OwnedCards.Add(int.Parse(DragCard.name));

                    break;
                case "Preast":
                    PlayerCreation.playerPriest.activeSupply.Add(int.Parse(DragAndDrop.card.name));
                    PlayerCreation.playerPriest.activeSupply.Remove(int.Parse(DragCard.name));
                    PlayerCreation.playerPriest.OwnedCards.Remove(int.Parse(DragAndDrop.card.name));
                    PlayerCreation.playerPriest.OwnedCards.Add(int.Parse(DragCard.name));

                    break;
            }
            DragCard.transform.SetParent(DragAndDrop.card.transform.parent);
            DragAndDrop.card.transform.SetParent(transform);
        }
    }
}
