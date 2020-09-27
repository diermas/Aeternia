using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    //All save states needed
    public static float[] savedWorldPosition;

    //ROGUE
    public static int rogueHealth;
    public static int[] roguePlayableDeck;
    public static int[] rogueActiveSupply;
    public static int[] rogueOwnedCards;

    //MAGE
    public static int mageHealth;
    public static int[] magePlayableDeck;
    public static int[] mageActiveSupply;
    public static int[] mageOwnedCards;

    //WARRIOR
    public static int warriorHealth;
    public static int[] warrioPlayableDeck;
    public static int[] warriorActiveSupply;
    public static int[] warriorOwnedCards;

    //PRIEST
    public static int priestHealth;
    public static int[] priestPlayableDeck;
    public static int[] priestActiveSupply;
    public static int[] priestOwnedCards;

    public PlayerData(Player[] player)
    {
        //Get last Player Position
        savedWorldPosition = Trackable.playerCharacterPostion;

        for (int i = 0; i < player.Length; i++)
        {

            //ROGUE
            if (player[i].className == "Rogue")
            {
                roguePlayableDeck = new int[player[i].playableDeck.Count];
                rogueActiveSupply = new int[player[i].activeSupply.Count];
                rogueOwnedCards = new int[player[i].OwnedCards.Count];

                rogueHealth = player[i].health;

                for (int j = 0; j < player[i].playableDeck.Count; j++)
                {
                    roguePlayableDeck[j] = player[i].playableDeck[j];
                }

                for (int k = 0; k < player[i].activeSupply.Count; k++)
                {
                    rogueActiveSupply[k] = player[i].activeSupply[k];
                }

                for (int l = 0; l < player[i].OwnedCards.Count; l++)
                {
                    rogueOwnedCards[l] = player[i].OwnedCards[l];
                }
            }


            //MAGE
            if (player[i].className == "Mage")
            {
                magePlayableDeck = new int[player[i].playableDeck.Count];
                mageActiveSupply = new int[player[i].activeSupply.Count];
                mageOwnedCards = new int[player[i].OwnedCards.Count];

                mageHealth = player[i].health;

                for (int j = 0; j < player[i].playableDeck.Count; j++)
                {
                    magePlayableDeck[j] = player[i].playableDeck[j];
                }

                for (int k = 0; k < player[i].activeSupply.Count; k++)
                {
                    mageActiveSupply[k] = player[i].activeSupply[k];
                }

                for (int l = 0; l < player[i].OwnedCards.Count; l++)
                {
                    mageOwnedCards[l] = player[i].OwnedCards[l];
                }
            }


            //WARRIOR
            if (player[i].className == "Warrior")
            {
                warrioPlayableDeck = new int[player[i].playableDeck.Count];
                warriorActiveSupply = new int[player[i].activeSupply.Count];
                warriorOwnedCards = new int[player[i].OwnedCards.Count];

                warriorHealth = player[i].health;

                for (int j = 0; j < player[i].playableDeck.Count; j++)
                {
                    warrioPlayableDeck[j] = player[i].playableDeck[j];
                }

                for (int k = 0; k < player[i].activeSupply.Count; k++)
                {
                    warriorActiveSupply[k] = player[i].activeSupply[k];
                }

                for (int l = 0; l < player[i].OwnedCards.Count; l++)
                {
                    warriorOwnedCards[l] = player[i].OwnedCards[l];
                }
            }


            //PRIEST
            if (player[i].className == "Priest")
            {
                priestPlayableDeck= new int[player[i].playableDeck.Count];
                priestActiveSupply = new int[player[i].activeSupply.Count];
                priestOwnedCards= new int[player[i].OwnedCards.Count];

                priestHealth = player[i].health;
                for (int j = 0; j < player[i].playableDeck.Count; j++)
                {
                    priestPlayableDeck[j] = player[i].playableDeck[j];
                }

                for (int k = 0; k < player[i].activeSupply.Count; k++)
                {
                    priestActiveSupply[k] = player[i].activeSupply[k];
                }

                for (int l = 0; l < player[i].OwnedCards.Count; l++)
                {
                    priestOwnedCards[l] = player[i].OwnedCards[l];
                }
            }
        }
    }
}
