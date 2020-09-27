using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class GameController : MonoBehaviour
{
    public int roundValue = 1;
    int currentNumOfEnemies = 0;
    int turnvalue = 1;
    public GameObject playerSpace, gameSpace;
    [HideInInspector]
    public GameObject collidedTarget, lastGrabbed;
    [HideInInspector]
    public Player player1, player2, player3, player4;
    public Text classNameInfo, resourceNameandvalue, RNaVActiveSupply;
    public TextMeshProUGUI discardCardsText, destroyCardsText, ASNotEnoughResource, destroyCardsDeckCurrentlyIn;
    public Player activePlayer;
    public int cardSeleceted;
    public GameObject endTurnButton, activeSupplyButton, enemySpace, ActiveSupplyUI, MainUI, gameOverUI, winUI, ASbuyButton, discardSelectionUI, destroyCardSelectionUI, discardCardObjectPositon, destroyCardObjectPositon;
    public GameObject leftArrowBDiscardSelection, rightArrowBDiscardSelection, leftArrowBDestorySelection, rightArrowBDestroySelection;
    static drawScript DrawScriptRef;
    bool cardGrabbed, setActiveBool;
    [HideInInspector]
    public List<Enemy> listOfEnemies = new List<Enemy>();
    [HideInInspector]
    public List<Player> listOfPlayers = new List<Player>();
    [HideInInspector]
    public List<Player> listOfDeadPlayers = new List<Player>();
    private CardFunctions CardFunctionsRef;
    private ActiveSupply ActiveSupplyRef;
    private EnemyCreation EnemyCreationRef;
    public Dictionary<int, cardClass> copyOfCompleteDeckOfCards;
    public static string loadEnemy;
    public int currentEnemyID = 0;
    public bool sortEnemyTransforms = false, sortEnemyCardTransforms = false;

    void Start()
    {
        copyOfCompleteDeckOfCards = new Dictionary<int, cardClass>(CardsReadIn.CompleteDeckOfCards);
        //gets reference to DrawScript and CardFunctions//
        DrawScriptRef = GetComponent<drawScript>();
        CardFunctionsRef = GetComponent<CardFunctions>();
        ActiveSupplyRef = GetComponent<ActiveSupply>();
        EnemyCreationRef = GetComponent<EnemyCreation>();

        SortPlayers(PlayerCreation.listOfSelectedPlayers);

        PlayerActive();

        //creates the first slime
        if (loadEnemy == "Slime")
        {
            EnemyCreationRef.LargeSlimeCreation();
        }
        else if (loadEnemy == "Rat")
        {
            EnemyCreationRef.RatGameScene();
        }
        

        #region Set UIs
        MainUI.SetActive(true);
        ActiveSupplyUI.SetActive(false);
        discardSelectionUI.SetActive(false);
        destroyCardSelectionUI.SetActive(false);
        gameOverUI.SetActive(false);
        winUI.SetActive(false);
        #endregion

        UpdateEnemyTransforms();
    }

    void Update()
    {
        MainUIKeyPress();
        ActiveSupplyKeyPress();
    }

    void SortPlayers(List<Player> pList)
    {
        int[] nums = new int[4];
        int i = 0;
        foreach (Player p in pList)
        {
            nums[i] = p.turnOrderValue;
            i++;

            if (i > 3)
            {
                break;
            }
        }
        Array.Sort(nums);

        foreach (Player p in pList)
        {
            if (nums[0] == p.turnOrderValue)
            {
                player1 = p;
                listOfPlayers.Add(player1);
                player1.thisPlayer = Instantiate(player1.thisPlayer, playerSpace.transform);
                player1.thisPlayer.transform.localPosition += new Vector3(-120, -200, 1); //Its new Position
                player1.healthUI = player1.thisPlayer.transform.GetChild(1).GetComponent<TextMeshPro>();
                player1.healthUI.text = player1.health.ToString();
                player1.defenceUI = player1.thisPlayer.transform.GetChild(2).GetComponent<TextMeshPro>();
                player1.defenceUI.text = "";
                player1.thisPlayer.GetComponent<OnPlayerScript>().playerRef = player1;
                player1.resourceUI = resourceNameandvalue.gameObject;
                player1.activeSupplyResourceUI = RNaVActiveSupply.gameObject;
            }
            if (nums[1] == p.turnOrderValue)
            {
                player2 = p;
                listOfPlayers.Add(player2);
                player2.thisPlayer = Instantiate(player2.thisPlayer, playerSpace.transform);
                player2.thisPlayer.transform.localPosition += new Vector3(100, -200, 1); //Its new Position
                player2.healthUI = player2.thisPlayer.transform.GetChild(1).GetComponent<TextMeshPro>();
                player2.healthUI.text = player2.health.ToString();
                player2.defenceUI = player2.thisPlayer.transform.GetChild(2).GetComponent<TextMeshPro>();
                player2.defenceUI.text = "";
                player2.thisPlayer.GetComponent<OnPlayerScript>().playerRef = player2;
                player2.resourceUI = resourceNameandvalue.gameObject;
                player2.activeSupplyResourceUI = RNaVActiveSupply.gameObject;
            }
            if (nums[2] == p.turnOrderValue)
            {
                player3 = p;
                listOfPlayers.Add(player3);
                player3.thisPlayer = Instantiate(player3.thisPlayer, playerSpace.transform);
                player3.thisPlayer.transform.localPosition += new Vector3(-120, 165, 1); //Its new Position
                player3.healthUI = player3.thisPlayer.transform.GetChild(1).GetComponent<TextMeshPro>();
                player3.healthUI.text = player3.health.ToString();
                player3.defenceUI = player3.thisPlayer.transform.GetChild(2).GetComponent<TextMeshPro>();
                player3.defenceUI.text = "";
                player3.thisPlayer.GetComponent<OnPlayerScript>().playerRef = player3;
                player3.resourceUI = resourceNameandvalue.gameObject;
                player3.activeSupplyResourceUI = RNaVActiveSupply.gameObject;
            }
            if (nums[3] == p.turnOrderValue)
            {
                player4 = p;
                listOfPlayers.Add(player4);
                
                player4.thisPlayer = Instantiate(player4.thisPlayer, playerSpace.transform);
                player4.thisPlayer.transform.localPosition += new Vector3(100, 165, 1); //Its new Position
                player4.healthUI = player4.thisPlayer.transform.GetChild(1).GetComponent<TextMeshPro>();
                player4.healthUI.text = player4.health.ToString();
                player4.defenceUI = player4.thisPlayer.transform.GetChild(2).GetComponent<TextMeshPro>();
                player4.defenceUI.text = "";
                player4.thisPlayer.GetComponent<OnPlayerScript>().playerRef = player4;
                player4.resourceUI = resourceNameandvalue.gameObject;
                player4.activeSupplyResourceUI = RNaVActiveSupply.gameObject;
            }
        }
    }

    void ActiveSupplyKeyPress()
    {
        if (ActiveSupplyUI.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0)) //Left click (or primary click)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                if (Physics2D.Raycast(ray.origin, ray.direction))
                {
                    if (hit.transform.gameObject.GetComponent<OnCardScript>())
                    {
                        ASbuyButton.SetActive(true);
                        cardSeleceted = int.Parse(hit.transform.gameObject.name);
                        ASNotEnoughResource.gameObject.SetActive(false);
                    }
                    else if (!hit.transform.gameObject.GetComponent<OnCardScript>() && !ASbuyButton)
                    {
                        ASNotEnoughResource.gameObject.SetActive(false);
                        ASbuyButton.SetActive(false);
                    }
                }
            }
        }
    }

    void MainUIKeyPress()
    {
        if (MainUI.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0)) //Left click (or primary click)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                if (Physics2D.Raycast(ray.origin, ray.direction))
                {
                    if (hit.transform.gameObject.GetComponent<OnCardScript>())
                    {
                        lastGrabbed = hit.transform.gameObject;
                        cardGrabbed = true;
                    }
                }
            }
            if (cardGrabbed == true) //if we have a card grabbed from the hand
            {
                if (Input.GetKey(KeyCode.Mouse0)) //this is for whilst holding down the left mouse key
                {
                    Vector3 movedPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -25);
                    lastGrabbed.transform.position = movedPos;
                }
                if (Input.GetKeyUp(KeyCode.Mouse0)) //Letting go of the left mouse click (or primary click)
                {
                    //TAKES A TARGET
                    if (copyOfCompleteDeckOfCards[int.Parse(lastGrabbed.name)].takesTarget == true) //checks if the card being grabbed requires a target
                    {
                        GetCollidedTarget();
                        if (copyOfCompleteDeckOfCards[int.Parse(lastGrabbed.name)].targetType.Contains("Enemy") && collidedTarget != null) //checks what target the card is looking for
                        {
                            EnemyTargetTypeFunc();
                        }
                        else if (copyOfCompleteDeckOfCards[int.Parse(lastGrabbed.name)].targetType.Contains("Player") && collidedTarget != null)
                        {
                            PlayerTargetTypeFunc();
                        }
                        //Missed
                        else
                        {
                            lastGrabbed.transform.position = lastGrabbed.GetComponent<OnCardScript>().originalPos;
                            cardGrabbed = false;
                        }
                    }

                    //DOESN'T TAKE A TARGET
                    else
                    {
                        CardFunctionsRef.CardFunc(lastGrabbed, collidedTarget, activePlayer, null); //runs the card funciton
                        activePlayer.currentCardsInHand.Remove(int.Parse(lastGrabbed.name)); // removes the card from the hand
                        if (copyOfCompleteDeckOfCards[int.Parse(lastGrabbed.name)].isBuff == true)
                        {
                            DrawScriptRef.CardLookup(activePlayer); //refreshes the hand
                            resourceNameandvalue.text = activePlayer.resourceName + ": " + activePlayer.resourceValue; //refreshes resource values
                        }
                        else
                        {
                            activePlayer.grave.Add(int.Parse(lastGrabbed.name)); //adds the card to the grave after being played
                            DrawScriptRef.CardLookup(activePlayer); //refreshes the hand
                            resourceNameandvalue.text = activePlayer.resourceName + ": " + activePlayer.resourceValue; //refreshes resource values
                        }
                    }
                    cardGrabbed = false;
                    collidedTarget = null;
                }
            }
        }
    }

    void DiscardCardKeyPress()
    {
        if (discardSelectionUI.activeSelf == true)
        {

        }
    }

    void PlayerTargetTypeFunc()
    {
        if (collidedTarget.GetComponent<OnPlayerScript>())
        {
            CardFunctionsRef.CardFunc(lastGrabbed, collidedTarget, activePlayer, null); //runs the card funciton
            activePlayer.currentCardsInHand.Remove(int.Parse(lastGrabbed.name)); // removes the card from the hand
            if (copyOfCompleteDeckOfCards[int.Parse(lastGrabbed.name)].isBuff == true)
            {
                DrawScriptRef.CardLookup(activePlayer); //refreshes the hand
                resourceNameandvalue.text = activePlayer.resourceName + ": " + activePlayer.resourceValue; //refreshes resource values
            }
            else
            {
                activePlayer.grave.Add(int.Parse(lastGrabbed.name)); //adds the card to the grave after being played
                DrawScriptRef.CardLookup(activePlayer); //refreshes the hand
                resourceNameandvalue.text = activePlayer.resourceName + ": " + activePlayer.resourceValue; //refreshes resource values
            }
        }
        else
        {
            lastGrabbed.transform.position = lastGrabbed.GetComponent<OnCardScript>().originalPos;
            cardGrabbed = false;
        }
        collidedTarget = null;
    }

    void EnemyTargetTypeFunc()
    {
        if (collidedTarget.GetComponent<OnEnemyScript>())
        {
            CardFunctionsRef.CardFunc(lastGrabbed, collidedTarget, activePlayer, collidedTarget.GetComponent<OnEnemyScript>().enemyRef); //runs the card funciton
            activePlayer.currentCardsInHand.Remove(int.Parse(lastGrabbed.name)); // removes the card from the hand
            collidedTarget.GetComponent<Animator>().ResetTrigger("Idle");
            collidedTarget.GetComponent<Animator>().ResetTrigger("cardHoverEnter");
            if (copyOfCompleteDeckOfCards[int.Parse(lastGrabbed.name)].isBuff == true)
            {
                DrawScriptRef.CardLookup(activePlayer); //refreshes the hand
                resourceNameandvalue.text = activePlayer.resourceName + ": " + activePlayer.resourceValue; //refreshes resource values
            }
            else
            {
                activePlayer.grave.Add(int.Parse(lastGrabbed.name)); //adds the card to the grave after being played
                DrawScriptRef.CardLookup(activePlayer); //refreshes the hand
                resourceNameandvalue.text = activePlayer.resourceName + ": " + activePlayer.resourceValue; //refreshes resource values
            }
        }
        else
        {
            lastGrabbed.transform.position = lastGrabbed.GetComponent<OnCardScript>().originalPos;
            cardGrabbed = false;
        }
        collidedTarget = null;
    }

    void GetCollidedTarget()
    {
        if (lastGrabbed.GetComponent<OnCardScript>().collidedGameobject != null) //this takes the current card grabbed, and checks if the collided object is an enemy by seeing if it has an enemy script attached to it
        {
            collidedTarget = lastGrabbed.GetComponent<OnCardScript>().collidedGameobject;
        }
    }

    public void BuyCard()
    {
        if (activePlayer.resourceValue < copyOfCompleteDeckOfCards[cardSeleceted].cardCost)
        {
            ASNotEnoughResource.gameObject.SetActive(true);
            ASNotEnoughResource.text = "Not Enough " + activePlayer.resourceName;
            ASbuyButton.SetActive(false);
        }
        else if (activePlayer.resourceValue >= copyOfCompleteDeckOfCards[cardSeleceted].cardCost)
        {
            copyOfCompleteDeckOfCards[cardSeleceted].cardCount -= 1;
            activePlayer.grave.Add(cardSeleceted);
            activePlayer.resourceValue -= copyOfCompleteDeckOfCards[cardSeleceted].cardCost;
            ActiveSupplyRef.DisplayActiveSupply(activePlayer); //refresh
            activePlayer.activeSupplyResourceUI.GetComponent<Animator>().ResetTrigger("resourceChange");
            activePlayer.activeSupplyResourceUI.transform.GetChild(0).GetComponent<Text>().text = "-" + copyOfCompleteDeckOfCards[cardSeleceted].cardCost.ToString();
            activePlayer.activeSupplyResourceUI.GetComponent<Animator>().SetTrigger("resourceChange");
        }
    }

    public void PlayerActive()
    {
        if (turnvalue == 1)
        {
            activeSupplyButton.SetActive(true);
            activePlayer = player1; // A new active player is set

            if (player1.health <= 0)
            {
                turnvalue++;
                PlayerActive();
            }
            else
            {
                DisplayActivePlayer();
            }
        }
        else if (turnvalue == 2)
        {
            DrawScriptRef.discardCards(activePlayer);
            player1 = activePlayer;
            player1.resourceValue = 0;
            activePlayer = player2; // A new active player is set

            if (activePlayer.health <= 0)
            {
                turnvalue++;
                PlayerActive();
            }
            else
            {
                DisplayActivePlayer();
            }
        }
        else if (turnvalue == 3)
        {
            DrawScriptRef.discardCards(activePlayer);
            player2 = activePlayer;
            player2.resourceValue = 0;
            activePlayer = player3; // A new active player is set

            if (activePlayer.health <= 0)
            {
                turnvalue++;
                PlayerActive();
            }
            else
            {
                DisplayActivePlayer();
            }
        }
        else if (turnvalue == 4)
        {
            DrawScriptRef.discardCards(activePlayer);
            player3 = activePlayer;
            player3.resourceValue = 0;
            activePlayer = player4; // A new active player is set

            if (activePlayer.health <= 0)
            {
                turnvalue++;
                PlayerActive();
            }
            else
            {
                DisplayActivePlayer();
            }
        }
        else if (turnvalue == 5) //Enemy Turn
        {
            bool healthChange = false;
            DrawScriptRef.discardCards(activePlayer);
            player4 = activePlayer;
            player4.resourceValue = 0;
            activeSupplyButton.SetActive(false);
            endTurnButton.SetActive(false);

            foreach (Enemy e in listOfEnemies)
            {
                e.ClearDefence();
                if (e.poisonDropOffDamage > 0)
                {
                    e.health -= e.poisonDropOffDamage;
                    e.thisEnemy.transform.GetChild(0).GetComponent<TextMeshPro>().text = e.poisonDropOffDamage + " Poison Damage";
                    e.thisEnemy.transform.GetComponent<Animator>().SetTrigger("damageDisplay");
                    e.poisonDropOffDamage -= 1;
                    healthChange = true;
                    if (e.poisonDropOffDamage == 0)
                    {
                        e.thisEnemy.transform.GetChild(2).gameObject.SetActive(false);
                    }
                }
            }

            if (healthChange == true)
            {
                CardFunctionsRef.HealthRefresh(listOfEnemies, listOfPlayers);
            }

            DrawScriptRef.StartCoroutine(DrawScriptRef.WaitDisplayEnemyCards(listOfEnemies));
            resourceNameandvalue.text = "";
            classNameInfo.text = "Enemy's Turn";
        }
        else if (turnvalue == 6) //Resets
        {
            roundValue += 1;
            DrawScriptRef.clearEnemyHand();
            //Restores the targetability of each player and adds each player to the targetable list
            foreach (Player p in listOfPlayers)
            {
                if (p.nextTargetable == roundValue && p.targetable == false)
                {
                    p.targetable = true;
                }
                //Reset Round Buffs Here
                p.ClearBuffs();

                if (p.poisonCount > 0)
                {
                    p.health -= p.poisonCount;
                    p.thisPlayer.transform.GetChild(3).GetComponent<TextMeshPro>().text = "-" + p.poisonCount.ToString() + " Poison Damage";
                    p.thisPlayer.GetComponent<Animator>().SetTrigger("takeDamage1");
                    p.poisonCount -= 1;
                }
            }

            //Applies drop off Damage
            foreach (Enemy e in listOfEnemies)
            {
                e.ClearBuffs();
                e.NewTurn();
            }

            turnvalue = 1;
            PlayerActive();
        }
    }

    public void ShowActiveSupply()
    {
        cardSeleceted = -1;
        setActiveBool = !setActiveBool;
        ActiveSupplyUI.SetActive(setActiveBool);
        MainUI.SetActive(!setActiveBool);
        if (setActiveBool == true)
        {
            ActiveSupplyRef.DisplayActiveSupply(activePlayer);
            activePlayer.activeSupplyResourceUI.GetComponent<Animator>().ResetTrigger("resourceChange");
            activePlayer.activeSupplyResourceUI.transform.GetChild(0).GetComponent<Text>().text = "";
        }
        if (setActiveBool == false)
        {
            ASbuyButton.SetActive(false);
            resourceNameandvalue.text = activePlayer.resourceName + ": " + activePlayer.resourceValue;
            activePlayer.resourceUI.GetComponent<Animator>().ResetTrigger("resourceChange");
            activePlayer.resourceUI.transform.GetChild(0).GetComponent<Text>().text = ""; 
        }
    }

    public void Stun(Player p)
    {
        p.stunValue -= 1;
        StartCoroutine(StunScreen(p));
    }

    private IEnumerator StunScreen(Player p)
    {
        activeSupplyButton.SetActive(false);
        endTurnButton.SetActive(false);
        DrawScriptRef.cardHand.SetActive(false);
        classNameInfo.text = p.className + " is Stunned";
        yield return new WaitForSeconds(2.0f);
        activeSupplyButton.SetActive(true);
        endTurnButton.SetActive(true);
        DrawScriptRef.cardHand.SetActive(true);
        endTurn();
        PlayerActive();
    }

    private void DisplayActivePlayer()
    {
        activePlayer.resourceValue += activePlayer.resourceNewTurnAddition;
        classNameInfo.text = activePlayer.className;
        DrawScriptRef.drawCards(ref activePlayer);
        resourceNameandvalue.text = activePlayer.resourceName + ": " + activePlayer.resourceValue;
        RNaVActiveSupply.text = activePlayer.resourceName + ": " + activePlayer.resourceValue;

        if (activePlayer.stunValue > 0)
        {
            Stun(activePlayer);
        }
    }

    public void endTurn()
    {
        turnvalue += 1;
    }

    public void UpdateEnemyTransforms()
    {
        if (listOfEnemies.Count != currentNumOfEnemies)
        {
            if (sortEnemyTransforms && listOfEnemies.Count > 0)
            {
                //Check if Enemies need ordering 
                int lastID = -1;
                bool sort = false;
                foreach (Enemy e in listOfEnemies)
                {
                    if (e.EnemyID < lastID)
                    {
                        sort = true;
                    }
                    else
                    {
                        lastID = e.EnemyID;
                    }
                }
                if (sort == true)
                {
                    listOfEnemies = listOfEnemies.OrderBy(e => e.EnemyID).ToList();
                }

                int currentEnemyPos = 0;
                foreach (Enemy e in listOfEnemies)
                {
                    e.EnemyTransformGameScene(listOfEnemies.Count, currentEnemyPos);
                    currentEnemyPos += 1;
                }
            }
            currentNumOfEnemies = listOfEnemies.Count;
        }
    }

}
