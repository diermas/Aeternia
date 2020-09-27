using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActiveSupply : MonoBehaviour
{
    public GameObject ActiveSupplyCards;
    private GameObject cardPos, cardCost;
    public GameController GameControllerRef;

    public void Start()
    {
        GameControllerRef = GetComponent<GameController>();
    }
    public void DisplayActiveSupply(Player p)
    {
        GameControllerRef.RNaVActiveSupply.text = p.resourceName + ": " + p.resourceValue;
        foreach (Transform child in ActiveSupplyCards.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        int lastValue = 0;
        Vector3 SavedPos = new Vector3(-640, 0, 0);
        Vector3 TextPos = new Vector3(-640, -250, 0);
        cardCost = new GameObject();
        cardCost.AddComponent<TextMeshPro>();
        foreach (int cardNum in p.activeSupply)
        {
            if (GameControllerRef.copyOfCompleteDeckOfCards[cardNum].cardCount > 0)
            {
                if (cardNum != lastValue)
                {
                    //Instansiates the Cards
                    cardPos = Instantiate(p.cardPrefab, ActiveSupplyCards.transform);
                    TextMeshPro[] elements = cardPos.GetComponentsInChildren<TextMeshPro>();
                    elements[0].text = GameControllerRef.copyOfCompleteDeckOfCards[cardNum].cardCost.ToString();
                    elements[0].gameObject.GetComponent<TextMeshPro>().sortingOrder = 251;
                    elements[1].text = GameControllerRef.copyOfCompleteDeckOfCards[cardNum].title;
                    elements[1].gameObject.GetComponent<TextMeshPro>().sortingOrder = 251;
                    elements[2].text = GameControllerRef.copyOfCompleteDeckOfCards[cardNum].cardEffectText;
                    elements[2].gameObject.GetComponent<TextMeshPro>().sortingOrder = 251;
                    elements[3].text = GameControllerRef.copyOfCompleteDeckOfCards[cardNum].subClass;
                    elements[3].gameObject.GetComponent<TextMeshPro>().sortingOrder = 251;
                    cardPos.transform.localPosition = SavedPos;
                    cardPos.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 250;
                    cardPos.transform.localScale = new Vector3(120, 120, 1);
                    cardPos.AddComponent<OnCardScript>();
                    cardPos.GetComponent<OnCardScript>().activeSupply = true;
                    cardPos.name = cardNum.ToString();

                    //Instansiates the Card Cost Text//
                    GameObject cardCostText = Instantiate(cardCost, ActiveSupplyCards.transform);
                    cardCostText.transform.localPosition = TextPos;
                    cardCostText.GetComponent<TextMeshPro>().text = "Card Count: " + GameControllerRef.copyOfCompleteDeckOfCards[cardNum].cardCount.ToString();
                    cardCostText.GetComponent<TextMeshPro>().sortingOrder = 251;
                    cardCostText.GetComponent<TextMeshPro>().autoSizeTextContainer = true;
                    cardCostText.transform.localScale = new Vector3(10, 10, 1);
                    SavedPos += new Vector3(320, 0, 0);
                    TextPos += new Vector3(320, 0, 0);
                    lastValue = cardNum;
                }
            }
        }
        Destroy(cardCost);
    }
}
