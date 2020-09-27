using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
public class ButtonCalling : MonoBehaviour
{
    private int MaxSupply = 5;
    public GameObject CardColectionLog, cardMask, ActiveColLog;
    private GameObject cardPos, cardColPos;
    public GameObject ActiveList;
    bool cardGrabbed;
    Player current;
    void Start()
    {
        
    }
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.P))
        {
            IncreseMax();
        }*/
    }
    public void IncreseMax()
    {
        GameObject newActive = new GameObject();
        MaxSupply++;
        ActiveColLog.GetComponent<GridLayoutGroup>().spacing = new Vector2(20, 20);
        ActiveColLog.GetComponent<GridLayoutGroup>().cellSize = new Vector2(255, 360);
        newActive.AddComponent<Image>();
        newActive.GetComponent<Image>().color = ActiveColLog.transform.GetChild(0).GetComponent<Image>().color;
        newActive.AddComponent<ActiveChange>();
        newActive.AddComponent<GridLayoutGroup>();
        newActive.GetComponent<GridLayoutGroup>().cellSize = new Vector2(25,35);
        newActive.GetComponent<GridLayoutGroup>().childAlignment = ActiveColLog.transform.GetChild(0).GetComponent<GridLayoutGroup>().childAlignment;

        newActive.transform.SetParent(ActiveColLog.transform);
        

    }
    public void RougeButtenCall()
    {
        CardColectionLog.GetComponent<WholeCelectionDrop>().activesupplyPlayer = "Rouge";
        current = PlayerCreation.playerRogue;
        GetColection(current);
        GetActiveSupplyCards(current);
    }
    public void PriestButtenCall()
    {
        CardColectionLog.GetComponent<WholeCelectionDrop>().activesupplyPlayer = "Preast";
        current = PlayerCreation.playerPriest;
        GetActiveSupplyCards(current);
        GetColection(current);
    }
    public void MageButtenCall()
    {
        CardColectionLog.GetComponent<WholeCelectionDrop>().activesupplyPlayer = "Mage";
        current = PlayerCreation.playerMage;
        GetActiveSupplyCards(current);
        GetColection(current);
    }
    public void WariorButtenCall()
    {
        CardColectionLog.GetComponent<WholeCelectionDrop>().activesupplyPlayer = "Warior";
        current = PlayerCreation.playerWarrior;
        GetActiveSupplyCards(current);
        GetColection(current);
    }


    void GetActiveSupplyCards(Player CurrentDeck)
    {
        foreach (Transform child in ActiveColLog.transform)
        {
            foreach (Transform AditionalChile in child.transform)
            {
                GameObject.Destroy(AditionalChile.gameObject);
            }
        }
        int Active = 0;
        int lastValue = -1;
        foreach (int Card in CurrentDeck.activeSupply)
        {
            ActiveColLog.transform.GetChild(Active).GetComponent<ActiveChange>().activesupplyPlayer = CardColectionLog.GetComponent<WholeCelectionDrop>().activesupplyPlayer;
            if (Card != lastValue)
            {
                //Instansiates the Cards
                cardPos = Instantiate(CurrentDeck.UIprefab, ActiveColLog.transform.GetChild(Active));
                TextMeshProUGUI[] elements = cardPos.GetComponentsInChildren<TextMeshProUGUI>();
                elements[0].text = CurrentDeck.completeDeck[Card].cardCost.ToString();
                elements[1].text = CurrentDeck.completeDeck[Card].title;
                elements[2].text = CurrentDeck.completeDeck[Card].cardEffectText;
                elements[3].text = CurrentDeck.completeDeck[Card].subClass;
                cardPos.transform.localScale = new Vector3(10, 10, 1);
                cardPos.AddComponent<OnCardScript>();
                cardPos.AddComponent<DragAndDrop>();
                cardPos.GetComponent<DragAndDrop>().Mask = cardMask;
                cardPos.AddComponent<CanvasGroup>();
                cardPos.name = Card.ToString();

                lastValue = Card;
                Active++;
            }
        }
    }

    void GetColection(Player CurrentDeck)
    {
        foreach (Transform child in CardColectionLog.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        int lastValue = -1;
        foreach (int Card in CurrentDeck.OwnedCards)
        {
            if (Card != lastValue)
            {
                //Instansiates the Cards
                cardColPos = Instantiate(CurrentDeck.UIprefab, CardColectionLog.transform);
                TextMeshProUGUI[] elements = cardColPos.GetComponentsInChildren<TextMeshProUGUI>();
                elements[0].text = CurrentDeck.completeDeck[Card].cardCost.ToString();
                elements[1].text = CurrentDeck.completeDeck[Card].title;
                elements[2].text = CurrentDeck.completeDeck[Card].cardEffectText;
                elements[3].text = CurrentDeck.completeDeck[Card].subClass;
                cardColPos.transform.localScale = new Vector3(10, 10, 1);
                cardColPos.AddComponent<OnCardScript>();
                cardColPos.AddComponent<CanvasGroup>();
                cardColPos.AddComponent<DragAndDrop>();
                cardColPos.GetComponent<DragAndDrop>().Mask = cardMask;
                cardColPos.name = Card.ToString();

                lastValue = Card;
            }
        }
    }
}
