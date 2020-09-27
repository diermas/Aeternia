using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : ScriptableObject
{
    public Sprite characterSprite;
   
    public bool cardsInitalized;
    public string className, resourceName;
    public GameObject cardPrefab, UIprefab, thisPlayer, resourceUI, activeSupplyResourceUI;
    public Dictionary<int, cardClass> completeDeck;
    public bool targetable = true;
    public TextMeshPro healthUI, defenceUI;
    public List<Buffs> BuffList;
    public int nextTargetable = 0; // A value used to store when the player becomes next targetable.
    public int health, resourceValue, defence, stunValue = 0, resourceNewTurnAddition = 0;
    public int firstValue, lastValue, turnOrderValue, maxHealth;
    public int rage, divinity, shadows, poisonCount;
    public int maxRage, maxDivinity, maxShadows, maxPoison;

    public List<int> currentCardsInHand; //this is the cards in hand (in game), may be removed from Player script in the future.
    public List<int> grave; // this is used for in play only (make be removed from player in the future
    public List<int> activeSupply; //this is the list of cards the plaeyr ahs availbe to them in the active supply
    public List<int> playableDeck; //These are the list of cards the player has available to them during the game
    public List<int> activeDeck; //These are the list of cards the player has available to when in a battle (includes cards in the graveyard).
    public List<int> cardsNotInPlayableDeck; //This is complete deck - playabledeck.
    public List<int> OwnedCards;

    public Player()
    {
        BuffList = new List<Buffs>();
        grave = new List<int>();
        completeDeck = new Dictionary<int, cardClass>();
        currentCardsInHand = new List<int>();
        activeSupply = new List<int>();
        playableDeck = new List<int>();
        cardsNotInPlayableDeck = new List<int>();
        activeDeck = new List<int>();
        OwnedCards = new List<int>();
        rage = 0;
        divinity = 0;
        shadows = 0;
        maxRage = 10;
        maxDivinity = 20;
        maxShadows = 6;
        maxPoison = 10;
    }

    // Functions to increase class specific resources, while keeping them under the set max value.

        //All values accounted for on a new turn e.g. poison drop off etc..
    public void NextTurn()
    {
        //Currently Unused.
    }
    public void ClearBuffs()
    {
        if (BuffList.Count > 0)
        {
            BuffList.Clear();
        }
        defence = 0;
        defenceUI.text = "";
    }
    public void AddDivinity(int amount)
    {
        divinity += amount;
        if (divinity > maxDivinity)
        {
            divinity = maxDivinity;
        }
    }
    public void AddRage(int amount)
    {
        rage += amount;
        if (rage > maxRage)
        {
            rage = maxRage;
        }
    }
    public void AddShadows(int amount)
    {
        shadows += amount;
        if (shadows > maxShadows)
        {
            shadows = maxShadows;
        }
    }
    public void LoseHealth(int amount)
    {
        health -= amount;
        if (health < 0)
        {
            health = 0;
        }
    }
    public void AddPoison(int amount)
    {
        if (amount + poisonCount >= maxPoison)
        {
            poisonCount = maxPoison;
        }
        else
        {
            poisonCount += amount;
        }
    }
}
