using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class drawScript : MonoBehaviour
{
    int savedPosition = 0;
    bool inGrave, inCardsInHand;
    public string decksToDestroyFrom;
    public GameObject[] allCards;
    public GameObject enemyCard, CardSelection;
    public GameObject slime, cardHand, enemyHand;
    public int discardValue, iDDiscardCardSelected, destroyValue, iDDestroyCardSelected;
    CardFunctions CardFunctionsRef;
    GameController GameControllerRef;

    public void Start()
    {
        GameControllerRef = GetComponent<GameController>();
        CardFunctionsRef = GetComponent<CardFunctions>();
    }

    public void randomiseCards(ref Player p)
    {
        if (p.cardsInitalized == true)
        {
            p.playableDeck.AddRange(p.grave);
            p.grave.Clear();
        }
        List<int> temp1 = new List<int>();
        List<int> temp2 = new List<int>();
        // copy of deck into temp1 // 
        foreach (int i in p.playableDeck)
        {
            temp1.Add(i);
        }
        while (temp2.Count != p.playableDeck.Count)
        {
            int rnd = Random.Range(0, temp1.Count);
            temp2.Add(temp1[rnd]);
            temp1.RemoveAt(rnd);
        }
        p.playableDeck = temp2;
    }

    public void spawnCardsInhand(GameObject[] allCards)
    {
        Vector3 lastCardposition = new Vector3(1600, 150, 0);

        for (int i = 0; i < allCards.Length; i++)
        {
            allCards[i].transform.position = (lastCardposition - new Vector3(200, 0, 0));
            lastCardposition = allCards[i].transform.position;
            allCards[i].AddComponent<OnCardScript>();
            if (GameControllerRef.copyOfCompleteDeckOfCards[int.Parse(allCards[i].name)].takesTarget == true)
            {
                if (GameControllerRef.copyOfCompleteDeckOfCards[int.Parse(allCards[i].name)].targetType == "Enemy")
                {
                    allCards[i].GetComponent<OnCardScript>().targetableEnemy = true;
                }
            }
        }
    }

    public void drawCards(ref Player p)
    {
        if (p.cardsInitalized == false)
        {
            randomiseCards(ref p);
            p.cardsInitalized = true;
        }
        for (int i = 0; i < 5; i++)
        {
            if (p.playableDeck.Count == 0)
            {
                randomiseCards(ref p);
            }
            p.currentCardsInHand.Add(p.playableDeck[0]);
            p.playableDeck.RemoveAt(0);
        }
        CardLookup(p);
    }

    public void clearEnemyHand()
    {
        foreach (Transform child in enemyHand.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void discardCards(Player p)
    {
        {
            foreach (Transform child in cardHand.transform)
            {
                Destroy(child.gameObject);
            }

            foreach (int i in p.currentCardsInHand)
            {
                p.grave.Add(i);
            }
            p.currentCardsInHand.Clear();
        }
    }

    public void CardLookup(Player p)
    {
        foreach (Transform child in cardHand.transform)
        {
            Destroy(child.gameObject);
        }
        allCards = new GameObject[p.currentCardsInHand.Count];
        int i = 0;
        foreach (int cardReference in p.currentCardsInHand)
        {
            allCards[i] = Instantiate(p.cardPrefab, cardHand.transform);
            TextMeshPro[] elements = allCards[i].GetComponentsInChildren<TextMeshPro>();
            elements[0].text = p.completeDeck[cardReference].cardCost.ToString();
            elements[1].text = p.completeDeck[cardReference].title;
            elements[2].text = p.completeDeck[cardReference].cardEffectText;
            elements[3].text = p.completeDeck[cardReference].subClass;
            allCards[i].name = cardReference.ToString();
            i++;
        }
        spawnCardsInhand(allCards);
       
    }

    public IEnumerator WaitDisplayEnemyCards(List<Enemy> eList)
    {
        List<Enemy> tempEList = new List<Enemy>();

        tempEList.AddRange(eList);

        int currentCardPos = 0;
        foreach (Enemy e in tempEList)
        {
            if (e.stunned == true)
            {
                if (e.frozen == false)
                {
                    e.thisEnemy.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Stunned!";
                    e.thisEnemy.transform.GetComponent<Animator>().SetTrigger("damageDisplay");
                    yield return new WaitForSeconds(1.5f);
                    e.stunned = false;
                    e.IconBarRefresh();
                }
                else
                {
                    e.thisEnemy.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Stunned & Frozen!";
                    e.thisEnemy.transform.GetComponent<Animator>().SetTrigger("damageDisplay");
                    yield return new WaitForSeconds(1.5f);
                    e.frozen = false;
                    e.stunned = false;
                    e.IconBarRefresh();
                }
            }
            else
            {
                if (e.frozen == false)
                {
                    int rnd = Random.Range(1, 100);
                    for (int i = e.firstCardValue; i <= e.lastCardValue; i++)
                    {
                        if (rnd >= CardsReadIn.enemyDecks[i].rangeMin && rnd <= CardsReadIn.enemyDecks[i].rangeMax)
                        {
                            e.currentCards.Add(CardsReadIn.enemyDecks[i].cardNumber);
                        }
                    }
                    float timeNeeded = 1.0f;
                    EnemyCardLookup(e, currentCardPos, ref timeNeeded);
                    currentCardPos += 1;
                    yield return new WaitForSeconds(timeNeeded);
                }
                else
                {
                    e.thisEnemy.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Frozen!";
                    e.thisEnemy.transform.GetComponent<Animator>().SetTrigger("damageDisplay");
                    yield return new WaitForSeconds(1.5f);
                    e.frozen = false;
                    e.IconBarRefresh();
                }
            }
        }
        currentCardPos = 0;

        tempEList.Clear();

        if (EnemyCreation.deleteEnemy == true)
        {
            foreach (Enemy eEnemy in eList)
            {
                if (EnemyCreation.deleteEnemyID.Contains(eEnemy.EnemyID))
                {
                    Destroy(eEnemy.thisEnemy);
                }
                else
                {   
                    tempEList.Add(eEnemy);
                }
            }
            GameControllerRef.listOfEnemies.Clear();
            GameControllerRef.listOfEnemies.AddRange(tempEList);
            EnemyCreation.deleteEnemy = false;
        }


        CardFunctionsRef.HealthRefresh(GameControllerRef.listOfEnemies, GameControllerRef.listOfPlayers);

        GameControllerRef.endTurnButton.SetActive(true);
    }

    public void EnemyCardLookup(Enemy e, int currentCardPos, ref float timeNeeded)
    {
        int tempCardCount = GameControllerRef.listOfEnemies.Count;
        int cardNum = e.currentCards[0];
        {
            enemyCard = Instantiate(slime, enemyHand.transform);
            TextMeshPro[] elements = enemyCard.GetComponentsInChildren<TextMeshPro>();
            elements[0].text = CardsReadIn.enemyDecks[cardNum].title;
            elements[1].text = CardsReadIn.enemyDecks[cardNum].cardEffectText;
            elements[2].text = CardsReadIn.enemyDecks[cardNum].subClass;
            if (GameControllerRef.sortEnemyCardTransforms)
            {
                e.CardTransformGameScene(tempCardCount, currentCardPos, enemyCard);
            }
            else
            {
                enemyCard.transform.localPosition += new Vector3(0, -350, 0) + e.cardPosition;
            }
        }
        e.currentCards.Clear();
        CardFunctionsRef.EnemyCardFunc(cardNum, GameControllerRef.listOfPlayers, e, ref timeNeeded);
    }


    #region Discard Cards Display Section
    public void DiscardCardScreenDisplay(Player p)
    {
        if (p.currentCardsInHand.Count == 0)
        {
            GameControllerRef.discardSelectionUI.SetActive(false);
            GameControllerRef.MainUI.SetActive(true);
        }
        else
        {
            GameControllerRef.MainUI.SetActive(false);
            GameControllerRef.discardSelectionUI.SetActive(true);
            GameControllerRef.discardCardsText.text = "Cards To Discard: " + discardValue;

            if (savedPosition == p.currentCardsInHand.Count - 1)
            {
                GameControllerRef.rightArrowBDiscardSelection.SetActive(false);
                GameControllerRef.leftArrowBDiscardSelection.SetActive(true);
            }
            else if (savedPosition == 0)
            {
                GameControllerRef.rightArrowBDiscardSelection.SetActive(true);
                GameControllerRef.leftArrowBDiscardSelection.SetActive(false);
            }
            else
            {
                GameControllerRef.rightArrowBDiscardSelection.SetActive(true);
                GameControllerRef.leftArrowBDiscardSelection.SetActive(true);
            }

            //clears previous card//
            foreach (Transform child in GameControllerRef.discardCardObjectPositon.transform)
            {
                Destroy(child.gameObject);
            }

            iDDiscardCardSelected = p.currentCardsInHand[savedPosition];
            CardSelection = Instantiate(p.cardPrefab, GameControllerRef.discardCardObjectPositon.transform);
            TextMeshPro[] elements = CardSelection.GetComponentsInChildren<TextMeshPro>();
            elements[0].text = p.completeDeck[iDDiscardCardSelected].cardCost.ToString();
            elements[1].text = p.completeDeck[iDDiscardCardSelected].title;
            elements[2].text = p.completeDeck[iDDiscardCardSelected].cardEffectText;
            elements[3].text = p.completeDeck[iDDiscardCardSelected].subClass;
        }
    }

    public void RightArrowDiscard()
    {
        savedPosition += 1;
        DiscardCardScreenDisplay(GameControllerRef.activePlayer);
    }

    public void LeftArrowDiscard()
    {
        savedPosition -= 1;
        DiscardCardScreenDisplay(GameControllerRef.activePlayer);
    }

    public void DiscardButton()
    {
        discardValue -= 1;

        GameControllerRef.activePlayer.grave.Add(iDDiscardCardSelected);
        GameControllerRef.activePlayer.currentCardsInHand.RemoveAt(savedPosition);
        
        if (discardValue == 0)
        {
            savedPosition = 0;
            GameControllerRef.discardSelectionUI.SetActive(false);
            CardLookup(GameControllerRef.activePlayer);
            GameControllerRef.MainUI.SetActive(true);
        }
        else
        {
            DiscardCardScreenDisplay(GameControllerRef.activePlayer);
        }
    }

    #endregion

    #region Destroy Cards Display Section

    public void DestroyCardScreenDisplay(Player p, string decks)
    {
        if (decksToDestroyFrom != decks)
        {
            iDDestroyCardSelected = 0;
            decksToDestroyFrom = decks;
            GameControllerRef.MainUI.SetActive(false);
            GameControllerRef.destroyCardSelectionUI.SetActive(true);
            GameControllerRef.gameSpace.transform.localPosition = new Vector3(2000, 0, 0);
        }
        
        GameControllerRef.destroyCardsText.text = "Cards To Destroy: " + destroyValue;

        //clears previous card//
        foreach (Transform child in GameControllerRef.destroyCardObjectPositon.transform)
        {
            Destroy(child.gameObject);
        }

        //cards from hand and discard pile
        if (decks == "grave and in hand")
        {
            if (p.grave.Count > 0 && p.currentCardsInHand.Count > 0)
            {
                #region Arrows
                if (savedPosition == p.grave.Count + p.currentCardsInHand.Count - 2)
                {
                    GameControllerRef.rightArrowBDestroySelection.SetActive(false);
                    GameControllerRef.leftArrowBDestorySelection.SetActive(true);
                }
                else if (savedPosition == 0)
                {
                    GameControllerRef.rightArrowBDestroySelection.SetActive(true);
                    GameControllerRef.leftArrowBDestorySelection.SetActive(false);
                }
                else
                {
                    GameControllerRef.rightArrowBDestroySelection.SetActive(true);
                    GameControllerRef.leftArrowBDestorySelection.SetActive(true);
                }
                #endregion

                if (savedPosition >= p.currentCardsInHand.Count)
                {
                    GameControllerRef.destroyCardsDeckCurrentlyIn.text = "(In Grave)";
                    inGrave = true;
                    inCardsInHand = false;
                    int tempPos = savedPosition;
                    tempPos -= p.currentCardsInHand.Count;
                    iDDestroyCardSelected = p.grave[tempPos];
                    CardSelection = Instantiate(p.cardPrefab, GameControllerRef.destroyCardObjectPositon.transform);
                    TextMeshPro[] elements = CardSelection.GetComponentsInChildren<TextMeshPro>();
                    elements[0].text = p.completeDeck[iDDestroyCardSelected].cardCost.ToString();
                    elements[1].text = p.completeDeck[iDDestroyCardSelected].title;
                    elements[2].text = p.completeDeck[iDDestroyCardSelected].cardEffectText;
                    elements[3].text = p.completeDeck[iDDestroyCardSelected].subClass;
                }
                else
                {
                    GameControllerRef.destroyCardsDeckCurrentlyIn.text = "(In Hand)";
                    inGrave = false ;
                    inCardsInHand = true;
                    iDDestroyCardSelected = p.currentCardsInHand[savedPosition];
                    CardSelection = Instantiate(p.cardPrefab, GameControllerRef.destroyCardObjectPositon.transform);
                    TextMeshPro[] elements = CardSelection.GetComponentsInChildren<TextMeshPro>();
                    elements[0].text = p.completeDeck[iDDestroyCardSelected].cardCost.ToString();
                    elements[1].text = p.completeDeck[iDDestroyCardSelected].title;
                    elements[2].text = p.completeDeck[iDDestroyCardSelected].cardEffectText;
                    elements[3].text = p.completeDeck[iDDestroyCardSelected].subClass;
                }
            }

            else if (p.grave.Count > 0 && p.currentCardsInHand.Count == 0)
            {
                #region Arrows
                if (savedPosition == p.grave.Count -1)
                {
                    GameControllerRef.rightArrowBDestroySelection.SetActive(false);
                    GameControllerRef.leftArrowBDestorySelection.SetActive(true);
                }
                else if (savedPosition == 0)
                {
                    GameControllerRef.rightArrowBDestroySelection.SetActive(true);
                    GameControllerRef.leftArrowBDestorySelection.SetActive(false);
                }
                else
                {
                    GameControllerRef.rightArrowBDestroySelection.SetActive(true);
                    GameControllerRef.leftArrowBDestorySelection.SetActive(true);
                }
                #endregion

                GameControllerRef.destroyCardsDeckCurrentlyIn.text = "(In Grave)";
                inGrave = true;
                inCardsInHand = false;
                iDDestroyCardSelected = p.grave[savedPosition];
                CardSelection = Instantiate(p.cardPrefab, GameControllerRef.destroyCardObjectPositon.transform);
                TextMeshPro[] elements = CardSelection.GetComponentsInChildren<TextMeshPro>();
                elements[0].text = p.completeDeck[iDDestroyCardSelected].cardCost.ToString();
                elements[1].text = p.completeDeck[iDDestroyCardSelected].title;
                elements[2].text = p.completeDeck[iDDestroyCardSelected].cardEffectText;
                elements[3].text = p.completeDeck[iDDestroyCardSelected].subClass;
            }

            else if (p.grave.Count == 0 && p.currentCardsInHand.Count > 0)
            {
                #region Arrows
                if (savedPosition == p.currentCardsInHand.Count - 1)
                {
                    GameControllerRef.rightArrowBDestroySelection.SetActive(false);
                    GameControllerRef.leftArrowBDestorySelection.SetActive(true);
                }
                else if (savedPosition == 0)
                {
                    GameControllerRef.rightArrowBDestroySelection.SetActive(true);
                    GameControllerRef.leftArrowBDestorySelection.SetActive(false);
                }
                else
                {
                    GameControllerRef.rightArrowBDestroySelection.SetActive(true);
                    GameControllerRef.leftArrowBDestorySelection.SetActive(true);
                }
                #endregion

                GameControllerRef.destroyCardsDeckCurrentlyIn.text = "(In Hand)";
                inGrave = false;
                inCardsInHand = true;
                iDDestroyCardSelected = p.currentCardsInHand[savedPosition];
                CardSelection = Instantiate(p.cardPrefab, GameControllerRef.destroyCardObjectPositon.transform);
                TextMeshPro[] elements = CardSelection.GetComponentsInChildren<TextMeshPro>();
                elements[0].text = p.completeDeck[iDDestroyCardSelected].cardCost.ToString();
                elements[1].text = p.completeDeck[iDDestroyCardSelected].title;
                elements[2].text = p.completeDeck[iDDestroyCardSelected].cardEffectText;
                elements[3].text = p.completeDeck[iDDestroyCardSelected].subClass;
            }
        }

        //cards just from the grave
        else if (decks == "grave")
        {

        }
        
        //cards just from in hand
        else if (decks == "in hand")
        {

        }

    }

    public void LeftArrowDestroy()
    {
        savedPosition -= 1;
        DestroyCardScreenDisplay(GameControllerRef.activePlayer, decksToDestroyFrom);
    }

    public void RightArrowDestroy()
    {
        savedPosition += 1;
        DestroyCardScreenDisplay(GameControllerRef.activePlayer, decksToDestroyFrom);
    }

    public void DestoryButton()
    {
        destroyValue -= 1;

        if (inGrave == true)
        {
            GameControllerRef.activePlayer.grave.RemoveAt(savedPosition - GameControllerRef.activePlayer.currentCardsInHand.Count);
        }
        else if (inCardsInHand == true)
        {
            GameControllerRef.activePlayer.currentCardsInHand.RemoveAt(savedPosition);
        }

        if (destroyValue == 0)
        {
            inGrave = false;
            inCardsInHand = false;
            decksToDestroyFrom = "";
            savedPosition = 0;
            GameControllerRef.destroyCardSelectionUI.SetActive(false);
            CardLookup(GameControllerRef.activePlayer);
            GameControllerRef.MainUI.SetActive(true);
            GameControllerRef.gameSpace.transform.localPosition = new Vector3(0, 0, 0);
        }
        else
        {
            DestroyCardScreenDisplay(GameControllerRef.activePlayer, decksToDestroyFrom);
        }
    }
    #endregion
}
