using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsReadIn : MonoBehaviour
{
    public cardClass CardClassRef;
    //initalise all decks as a dictionary///
    public static Dictionary<int, cardClass> enemyDecks = new Dictionary<int, cardClass>();
    public static Dictionary<int, cardClass> CompleteDeckOfCards = new Dictionary<int, cardClass>();

    public string[] cardDataRows, cardDataText;
    public int idNum;

    // public List<int> usedMageCards, usedWarriorCards, usedPriestCards, usedRougeCards, testList;

    void Start()
    {
        #region Player Read-In
        //read-in Mage deck//
        TextAsset mageReadIn = Resources.Load<TextAsset>("mageReadIn");
        cardDataRows = mageReadIn.text.Split('\n');

        for (int i = 1; i < cardDataRows.Length; i++)
        {
            cardDataText = cardDataRows[i].Split(',');
            idNum = int.Parse(cardDataText[0]);
            CompleteDeckOfCards.Add(idNum, ScriptableObject.CreateInstance<mageCards>());

            CompleteDeckOfCards[idNum].cardNumber = idNum;
            CompleteDeckOfCards[idNum].title = cardDataText[1];
            CompleteDeckOfCards[idNum].subClass = cardDataText[2];
            CompleteDeckOfCards[idNum].cardCost = int.Parse(cardDataText[3]);
            CompleteDeckOfCards[idNum].cardEffectText = cardDataText[4];
            CompleteDeckOfCards[idNum].cardCount = int.Parse(cardDataText[5]);
            CompleteDeckOfCards[idNum].takesTarget = bool.Parse(cardDataText[6]);
            CompleteDeckOfCards[idNum].numberOfTargets = int.Parse(cardDataText[7]);
            CompleteDeckOfCards[idNum].targetType = cardDataText[8];
            CompleteDeckOfCards[idNum].isBuff = bool.Parse(cardDataText[9]);
        }


        //read-in Warrior deck//
        TextAsset warriorReadIn = Resources.Load<TextAsset>("warriorReadIn");
        cardDataRows = warriorReadIn.text.Split('\n');

        for (int i = 1; i < cardDataRows.Length; i++)
        {
            cardDataText = cardDataRows[i].Split(',');
            idNum = int.Parse(cardDataText[0]);
            CompleteDeckOfCards.Add(idNum, ScriptableObject.CreateInstance<warriorCards>());

            CompleteDeckOfCards[idNum].cardNumber = idNum;
            CompleteDeckOfCards[idNum].title = cardDataText[1];
            CompleteDeckOfCards[idNum].subClass = cardDataText[2];
            CompleteDeckOfCards[idNum].cardCost = int.Parse(cardDataText[3]);
            CompleteDeckOfCards[idNum].cardEffectText = cardDataText[4];
            CompleteDeckOfCards[idNum].cardCount = int.Parse(cardDataText[5]);
            CompleteDeckOfCards[idNum].takesTarget = bool.Parse(cardDataText[6]);
            CompleteDeckOfCards[idNum].numberOfTargets = int.Parse(cardDataText[7]);
            CompleteDeckOfCards[idNum].targetType = cardDataText[8];
            CompleteDeckOfCards[idNum].isBuff = bool.Parse(cardDataText[9]);
        }

        //read-in Priest deck//
        TextAsset priestReadIn = Resources.Load<TextAsset>("priestReadIn");
        cardDataRows = priestReadIn.text.Split('\n');

        for (int i = 1; i < cardDataRows.Length; i++)
        {
            cardDataText = cardDataRows[i].Split(',');
            idNum = int.Parse(cardDataText[0]);
            CompleteDeckOfCards.Add(idNum, ScriptableObject.CreateInstance<priestCards>());

            CompleteDeckOfCards[idNum].cardNumber = idNum;
            CompleteDeckOfCards[idNum].title = cardDataText[1];
            CompleteDeckOfCards[idNum].subClass = cardDataText[2];
            CompleteDeckOfCards[idNum].cardCost = int.Parse(cardDataText[3]);
            CompleteDeckOfCards[idNum].cardEffectText = cardDataText[4];
            CompleteDeckOfCards[idNum].cardCount = int.Parse(cardDataText[5]);
            CompleteDeckOfCards[idNum].takesTarget = bool.Parse(cardDataText[6]);
            CompleteDeckOfCards[idNum].numberOfTargets = int.Parse(cardDataText[7]);
            CompleteDeckOfCards[idNum].targetType = cardDataText[8];
            CompleteDeckOfCards[idNum].isBuff = bool.Parse(cardDataText[9]);
        }

        //read-in Rogue deck//
        TextAsset rogueReadIn = Resources.Load<TextAsset>("rogueReadIn");
        cardDataRows = rogueReadIn.text.Split('\n');
        for (int i = 1; i < cardDataRows.Length; i++)
        {

            cardDataText = cardDataRows[i].Split(',');
            idNum = int.Parse(cardDataText[0]);
            CompleteDeckOfCards.Add(idNum, ScriptableObject.CreateInstance<rogueCards>());
            CompleteDeckOfCards[idNum].cardNumber = idNum;
            CompleteDeckOfCards[idNum].title = cardDataText[1];
            CompleteDeckOfCards[idNum].subClass = cardDataText[2];
            CompleteDeckOfCards[idNum].cardCost = int.Parse(cardDataText[3]);
            CompleteDeckOfCards[idNum].cardEffectText = cardDataText[4];
            CompleteDeckOfCards[idNum].cardCount = int.Parse(cardDataText[5]);
            CompleteDeckOfCards[idNum].takesTarget = bool.Parse(cardDataText[6]);
            CompleteDeckOfCards[idNum].numberOfTargets = int.Parse(cardDataText[7]);
            CompleteDeckOfCards[idNum].targetType = cardDataText[8];
            CompleteDeckOfCards[idNum].isBuff = bool.Parse(cardDataText[9]);

        }
        #endregion

        #region Enemy Read-In
        TextAsset enemyReadIn = Resources.Load<TextAsset>("enemyReadIn");
        cardDataRows = enemyReadIn.text.Split('\n');
        for (int i = 1; i < cardDataRows.Length; i++)
        {
            cardDataText = cardDataRows[i].Split(',');
            idNum = int.Parse(cardDataText[0]);
            enemyDecks.Add(idNum, ScriptableObject.CreateInstance<cardClass>());
            enemyDecks[idNum].cardNumber = idNum;
            enemyDecks[idNum].title = cardDataText[1];
            enemyDecks[idNum].subClass = cardDataText[2];
            enemyDecks[idNum].rangeMin = int.Parse(cardDataText[3]);
            enemyDecks[idNum].rangeMax = int.Parse(cardDataText[4]);
            enemyDecks[idNum].cardEffectText = cardDataText[5];
        }
        #endregion
    }
}
