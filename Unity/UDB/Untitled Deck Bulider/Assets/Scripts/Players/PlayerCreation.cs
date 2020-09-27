using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCreation : MonoBehaviour
{
    public static Player[] allClasses;
    public static List<Player> listOfSelectedPlayers = new List<Player>();
    public static Player playerRogue, playerMage, playerWarrior, playerPriest;
    public GameObject rogueCardPrefab, warriorCardPrefab, priestCardPrefab, mageCardPrefab, roguePrefabUI, warriorPrefabUI, priestPrefabUI, magePrefabUI;
    public GameObject rogueSpriteGO, warriorSpriteGO, priestSpriteGO, mageSpriteGO;
    public int test = 4;

    public void CreatePlayer()
    {
        RogueCreation();
        MageCreation();
        WarriorCreation();
        PriestCreation();
        allClasses = new Player[4];
    }

    public void RogueCreation()
    {
        playerRogue = ScriptableObject.CreateInstance<Player>();
        playerRogue.health = rogueCards.startingHealth;
        playerRogue.maxHealth = playerRogue.health;
        playerRogue.className = "Rogue";
        playerRogue.resourceName = rogueCards.resourceName;
        playerRogue.turnOrderValue = rogueCards.turnOrderValue;
        playerRogue.cardPrefab = rogueCardPrefab;
        playerRogue.UIprefab = roguePrefabUI;
        playerRogue.thisPlayer = rogueSpriteGO;
        playerRogue.thisPlayer.transform.GetChild(0).GetComponent<TextMeshPro>().text = playerRogue.className; //name

        //Assigns the decks (Complete Deck + Owned Cards)
        for (int i = rogueCards.firstCard; i <= rogueCards.lastCard; i++)
        {
            playerRogue.completeDeck.Add(i, CardsReadIn.CompleteDeckOfCards[i]);
        }

        //Starter Deck (Playable deck)
        for (int i = rogueCards.starterCardFV; i <= rogueCards.starterCardLV; i++)
        {
            for (int j = 0; j < CardsReadIn.CompleteDeckOfCards[i].cardCount; j++)
            {
                playerRogue.playableDeck.Add(CardsReadIn.CompleteDeckOfCards[i].cardNumber);
            }
        }

        //Assigns Starting Active Supply
        for (int cardVal = rogueCards.firstCard+3; cardVal <= rogueCards.lastCard; cardVal++)
        {
            switch (cardVal)
            {
                case 225: //203
                    playerRogue.activeSupply.Add(cardVal); //Done (NT)
                    break;

                case 204: //204
                    playerRogue.activeSupply.Add(cardVal); //Done (NT)
                    break;

                case 205:
                    playerRogue.activeSupply.Add(cardVal); //Done (NT)
                    break;

                case 208:
                    playerRogue.activeSupply.Add(cardVal); //Done (NT)
                    break;

                case 209:
                    playerRogue.activeSupply.Add(cardVal); //Done (NT)
                    break;
                default:
                    playerRogue.OwnedCards.Add(cardVal);
                    break;
            }
        }

        //Add current class to list 
        listOfSelectedPlayers.Add(playerRogue);

        // allClasses[0] = playerRogue;
    }

    public void MageCreation()
    {
        playerMage = ScriptableObject.CreateInstance<Player>();
        playerMage.health = mageCards.startingHealth;
        playerMage.maxHealth = playerMage.health;
        playerMage.className = "Mage";
        playerMage.resourceName = mageCards.resourceName;
        playerMage.turnOrderValue = mageCards.turnOrderValue;
        playerMage.cardPrefab = mageCardPrefab;
        playerMage.UIprefab = magePrefabUI;
        playerMage.thisPlayer = mageSpriteGO;
        playerMage.thisPlayer.transform.GetChild(0).GetComponent<TextMeshPro>().text = playerMage.className;

        //Assigns the decks
        for (int i = mageCards.firstCard; i <= mageCards.lastCard; i++)
        {
            playerMage.completeDeck.Add(i, CardsReadIn.CompleteDeckOfCards[i]);
        }

        //Starter Deck
        for (int i = mageCards.starterCardFV; i <= mageCards.starterCardLV; i++)
        {
            for (int j = 0; j < CardsReadIn.CompleteDeckOfCards[i].cardCount; j++)
            {
                playerMage.playableDeck.Add(CardsReadIn.CompleteDeckOfCards[i].cardNumber);
            }
        }

        //Assigns Starting Active Supply [NT = Not tested]
        for (int cardVal = mageCards.firstCard+3; cardVal <= mageCards.lastCard; cardVal++)
        {
            switch (cardVal)
            {
                case 6: 
                    playerMage.activeSupply.Add(cardVal); //Done (NT)
                    break;

                case 7:
                    playerMage.activeSupply.Add(cardVal); //Done (NT)
                    break;

                case 8:
                    playerMage.activeSupply.Add(cardVal); //Done (NT)
                    break;

                case 10:
                    playerMage.activeSupply.Add(cardVal); //Done (NT)
                    break;

                case 12:
                    playerMage.activeSupply.Add(cardVal); //Done (NT)
                    break;

                default:
                    playerMage.OwnedCards.Add(cardVal);
                    break;
            }

        }

        //Add current class to list 
        listOfSelectedPlayers.Add(playerMage);

        //   allClasses[1] = playerMage;
    }

    public void WarriorCreation()
    {
        playerWarrior = ScriptableObject.CreateInstance<Player>();
        playerWarrior.health = warriorCards.startingHealth;
        playerWarrior.maxHealth = playerWarrior.health;
        playerWarrior.className = "Warrior";
        playerWarrior.resourceName = warriorCards.resourceName;
        playerWarrior.turnOrderValue = warriorCards.turnOrderValue;
        playerWarrior.cardPrefab = warriorCardPrefab;
        playerWarrior.UIprefab = warriorPrefabUI;
        playerWarrior.thisPlayer = warriorSpriteGO;
        playerWarrior.thisPlayer.transform.GetChild(0).GetComponent<TextMeshPro>().text = playerWarrior.className;

        //Assigns the decks
        for (int i = warriorCards.firstCard; i <= warriorCards.lastCard; i++)
        {
            //     for (int j = 0; j < CardsReadIn.CompleteDeckOfCards[i].cardCount; j++)
            //    {
            playerWarrior.completeDeck.Add(i, CardsReadIn.CompleteDeckOfCards[i]);
            //     }


        }

        //Starter Deck
        for (int i = warriorCards.starterCardFV; i <= warriorCards.starterCardLV; i++)
        {
            for (int j = 0; j < CardsReadIn.CompleteDeckOfCards[i].cardCount; j++)
            {
                playerWarrior.playableDeck.Add(CardsReadIn.CompleteDeckOfCards[i].cardNumber);
            }
        }

        //Assigns Starting Active Supply
        for (int cardVal = warriorCards.firstCard+3; cardVal <= warriorCards.lastCard; cardVal++)
        {
            switch (cardVal)
            {
                case 403:
                    playerWarrior.activeSupply.Add(cardVal); //Not done
                    break;

                case 404:
                    playerWarrior.activeSupply.Add(cardVal); //Done (NT)
                    break;

                case 405:
                    playerWarrior.activeSupply.Add(cardVal); //Done (NT)
                    break;

                case 407:
                    playerWarrior.activeSupply.Add(cardVal); //Done (NT)
                    break;

                case 412:
                    playerWarrior.activeSupply.Add(cardVal); //Done (NT)
                    break;
                default:
                    playerWarrior.OwnedCards.Add(cardVal);
                    break;
            }

        }

        //Add current class to list 
        listOfSelectedPlayers.Add(playerWarrior);

        //   allClasses[2] = playerWarrior;

    }

    public void PriestCreation()
    {
        playerPriest = ScriptableObject.CreateInstance<Player>();
        playerPriest.health = priestCards.startingHealth;
        playerPriest.maxHealth = playerPriest.health;
        playerPriest.className = "Priest";
        playerPriest.resourceName = priestCards.resourceName;
        playerPriest.turnOrderValue = priestCards.turnOrderValue;
        playerPriest.cardPrefab = priestCardPrefab;
        playerPriest.UIprefab = priestPrefabUI;
        playerPriest.thisPlayer = priestSpriteGO;
        playerPriest.thisPlayer.transform.GetChild(0).GetComponent<TextMeshPro>().text = playerPriest.className;

        //Assigns the decks
        for (int i = priestCards.firstCard; i <= priestCards.lastCard; i++)
        {
            //   for (int j = 0; j < CardsReadIn.CompleteDeckOfCards[i].cardCount; j++)
            //   {
            playerPriest.completeDeck.Add(i, CardsReadIn.CompleteDeckOfCards[i]);
            //    }
        }

        //Starter Deck
        for (int i = priestCards.starterCardFV; i <= priestCards.starterCardLV; i++)
        {
            for (int j = 0; j < CardsReadIn.CompleteDeckOfCards[i].cardCount; j++)
            {
                playerPriest.playableDeck.Add(CardsReadIn.CompleteDeckOfCards[i].cardNumber);
            }
        }

        //Assigns Starting Active Supply
        for (int cardVal = priestCards.firstCard+3; cardVal <= priestCards.lastCard; cardVal++)
        {
            switch (cardVal)
            {
                case 603:
                    playerPriest.activeSupply.Add(cardVal); //Done (NT)
                    break;

                case 605:
                    playerPriest.activeSupply.Add(cardVal); //Done (NT)
                    break;

                case 606:
                    playerPriest.activeSupply.Add(cardVal); //Done (NT)
                    break;

                case 608:
                    playerPriest.activeSupply.Add(cardVal); //Done (NT)
                    break;

                case 610:
                    playerPriest.activeSupply.Add(cardVal);
                    break;

                default:
                    playerPriest.OwnedCards.Add(cardVal);
                    break;
            }

        }

        //Add current class to list 
        listOfSelectedPlayers.Add(playerPriest);

        //     allClasses[3] = playerPriest;
    }

    public void RetrivePlayerData()
    {
        RogueDataRetrieval();
        MageDataRetrieval();
        WarriorDataRetrieval();
        PriestDataRetrieval();
    }

    public void RogueDataRetrieval()
    {
        //Retrieval of Rogue Class//
        playerRogue = new Player();
        playerRogue.health = PlayerData.rogueHealth;
        playerRogue.className = "Rogue";
        playerRogue.resourceName = rogueCards.resourceName;
        playerRogue.turnOrderValue = rogueCards.turnOrderValue;
        playerRogue.cardPrefab = rogueCardPrefab;
        playerRogue.UIprefab = roguePrefabUI;

        //Assigns the decks
        for (int i = rogueCards.firstCard; i <= rogueCards.lastCard; i++)
        {
            playerRogue.completeDeck.Add(i, CardsReadIn.CompleteDeckOfCards[i]);
        }

        //Owned Cards
        for (int i = 0; i < PlayerData.rogueOwnedCards.Length; i++)
        {
            playerRogue.OwnedCards.Add(PlayerData.rogueOwnedCards[i]);
        }

        //PLayable Deck
        for (int i = 0; i < PlayerData.roguePlayableDeck.Length; i++)
        {
            playerRogue.playableDeck.Add(PlayerData.roguePlayableDeck[i]);
        }

        //Active Supply
        for (int i = 0; i < PlayerData.rogueActiveSupply.Length; i++)
        {
            playerRogue.activeSupply.Add(PlayerData.rogueActiveSupply[i]);
        }
    }

    public void MageDataRetrieval()
    {
        //Retrieval of Mage Class//
        playerMage = new Player();
        playerMage.health = PlayerData.mageHealth;
        playerMage.className = "Mage";
        playerMage.resourceName = mageCards.resourceName;
        playerMage.turnOrderValue = mageCards.turnOrderValue;
        playerMage.cardPrefab = mageCardPrefab;
        playerMage.UIprefab = magePrefabUI;

        //Assigns the decks
        for (int i = mageCards.firstCard; i <= mageCards.lastCard; i++)
        {
            playerMage.completeDeck.Add(i, CardsReadIn.CompleteDeckOfCards[i]);
        }

        //Owned Cards
        for (int i = 0; i < PlayerData.mageOwnedCards.Length; i++)
        {
            playerMage.OwnedCards.Add(PlayerData.mageOwnedCards[i]);
        }

        //PLayable Deck
        for (int i = 0; i < PlayerData.magePlayableDeck.Length; i++)
        {
            playerMage.playableDeck.Add(PlayerData.magePlayableDeck[i]);
        }

        //Active Supply
        for (int i = 0; i < PlayerData.mageActiveSupply.Length; i++)
        {
            playerMage.activeSupply.Add(PlayerData.mageActiveSupply[i]);
        }
    }

    public void WarriorDataRetrieval()
    {
        //Retrieval of Warrior Class//
        playerWarrior = new Player();
        playerWarrior.health = PlayerData.warriorHealth;
        playerWarrior.className = "Warrior";
        playerWarrior.resourceName = warriorCards.resourceName;
        playerWarrior.turnOrderValue = warriorCards.turnOrderValue;
        playerWarrior.cardPrefab = warriorCardPrefab;
        playerWarrior.UIprefab = warriorPrefabUI;

        //Assigns the decks
        for (int i = warriorCards.firstCard; i <= warriorCards.lastCard; i++)
        {
            playerWarrior.completeDeck.Add(i, CardsReadIn.CompleteDeckOfCards[i]);
        }

        //Owned Cards
        for (int i = 0; i < PlayerData.warriorOwnedCards.Length; i++)
        {
            playerWarrior.OwnedCards.Add(PlayerData.warriorOwnedCards[i]);
        }

        //PLayable Deck
        for (int i = 0; i < PlayerData.warrioPlayableDeck.Length; i++)
        {
            playerWarrior.playableDeck.Add(PlayerData.warrioPlayableDeck[i]);
        }

        //Active Supply
        for (int i = 0; i < PlayerData.warriorActiveSupply.Length; i++)
        {
            playerWarrior.activeSupply.Add(PlayerData.warriorActiveSupply[i]);
        }
    }

    public void PriestDataRetrieval()
    {
        //Retrieval of Warrior Class//
        playerPriest = new Player();
        playerPriest.health = PlayerData.priestHealth;
        playerPriest.className = "Priest";
        playerPriest.resourceName = priestCards.resourceName;
        playerPriest.turnOrderValue = priestCards.turnOrderValue;
        playerPriest.cardPrefab = priestCardPrefab;
        playerPriest.UIprefab = priestPrefabUI;

        //Assigns the decks
        for (int i = priestCards.firstCard; i <= priestCards.lastCard; i++)
        {
            playerPriest.completeDeck.Add(i, CardsReadIn.CompleteDeckOfCards[i]);
        }

        //Owned Cards
        for (int i = 0; i < PlayerData.priestOwnedCards.Length; i++)
        {
            playerPriest.OwnedCards.Add(PlayerData.priestOwnedCards[i]);
        }

        //PLayable Deck
        for (int i = 0; i < PlayerData.priestPlayableDeck.Length; i++)
        {
            playerPriest.playableDeck.Add(PlayerData.priestPlayableDeck[i]);
        }

        //Active Supply
        for (int i = 0; i < PlayerData.priestActiveSupply.Length; i++)
        {
            playerPriest.activeSupply.Add(PlayerData.priestActiveSupply[i]);
        }
    }
}
