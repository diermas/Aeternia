using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyCreation : MonoBehaviour
{
    GameController GameControllerRef;
    public static bool deleteEnemy = false;
    public static List<int> deleteEnemyID = new List<int>();

    public void CreateEnemy(Enemy ParentEnemy, string enemyCreationClassName)
    {
        if (enemyCreationClassName == "MediumSlime")
        {
            MediumSlimeCreation(ParentEnemy);
        }
        else if (enemyCreationClassName == "SmallSlime")
        {
            SmallSlimeCreation(ParentEnemy);
        }
        else if (enemyCreationClassName == "Rat")
        {
            RatCreation(ParentEnemy);
        }
    }

    void Start()
    {
        GameControllerRef = GetComponent<GameController>();
    }

    #region Slime Creation
    public void LargeSlimeCreation()
    {
        Enemy largeSlime = new Enemy();
        largeSlime.thisEnemy = Instantiate(Resources.Load<GameObject>("Prefabs/Sprites/Slime Prefab"), GameControllerRef.enemySpace.transform);
        largeSlime.thisEnemy.name = "LargeSlime";
        largeSlime.className = "LargeSlime";
        largeSlime.child = "MediumSlime";
        largeSlime.maxHealth = 30;
        largeSlime.EnemyID = GameControllerRef.currentEnemyID;
        GameControllerRef.currentEnemyID += 1;
        largeSlime.firstCardValue = SlimeClass.largeSlimeFirstCard;
        largeSlime.lastCardValue = SlimeClass.largeSlimeLastCard;
        largeSlime.health = SlimeClass.largeSlimeHealth;
        largeSlime.resistanceDamageTypes.Add(SlimeClass.physicalDmg, SlimeClass.resist);
        largeSlime.resistanceDamageTypes.Add(SlimeClass.magaicalDmg, SlimeClass.normal);
        largeSlime.resistanceDamageTypes.Add(SlimeClass.fireDmg, SlimeClass.normal);
        largeSlime.resistanceDamageTypes.Add(SlimeClass.iceDmg, SlimeClass.normal);
        largeSlime.resistanceDamageTypes.Add(SlimeClass.holyDmg, SlimeClass.normal);
        largeSlime.thisEnemy.AddComponent<OnEnemyScript>().enemyRef = largeSlime;
        largeSlime.classContainsDefence = false;

        for (int i = largeSlime.firstCardValue; i <= largeSlime.lastCardValue; i++)
        {
            largeSlime.deck.Add(CardsReadIn.enemyDecks[i].cardNumber);
        }

        GameControllerRef.listOfEnemies.Add(largeSlime);
        largeSlime.healthBarObject = Instantiate(Resources.Load<GameObject>("Prefabs/HP"), largeSlime.thisEnemy.transform);
        largeSlime.healthBarObject.transform.localPosition += new Vector3(0, 0.242f, 0);
        largeSlime.healthBarObject.transform.localScale = new Vector3(0.0015f, 0.0015f, 1);
        TextMeshPro T = largeSlime.healthBarObject.transform.GetChild(0).GetComponent<TextMeshPro>();
        T.text = largeSlime.health.ToString();
        T = largeSlime.healthBarObject.transform.GetChild(1).GetComponent<TextMeshPro>();
        T.text = largeSlime.className;
        largeSlime.iconBar = largeSlime.thisEnemy.transform.GetChild(2).gameObject;
    }

    public void MediumSlimeCreation(Enemy parentEnemy)
    {
        List<Enemy> tempE = new List<Enemy>();

        tempE.Add(new Enemy());
        tempE.Add(new Enemy());

        Vector3 newPos = new Vector3(-200, 0, 0);
        Vector3 newCardPos = new Vector3(parentEnemy.thisEnemy.transform.localPosition.x - 200, 0, 0);
        foreach (Enemy e in tempE)
        {
            TextMeshPro T = new TextMeshPro();
            e.thisEnemy = Instantiate(Resources.Load<GameObject>("Prefabs/Sprites/Slime Prefab"), GameControllerRef.enemySpace.transform);
            e.thisEnemy.transform.localPosition += newPos;
            newPos += new Vector3(400, 0, 0);
            e.cardPosition += newCardPos;
            newCardPos += new Vector3(400, 0, 0);
            e.thisEnemy.name = "MediumSlime";
            e.className = "MediumSlime";
            e.child = "SmallSlime";
            e.EnemyID = GameControllerRef.currentEnemyID;
            GameControllerRef.currentEnemyID += 1;
            e.maxHealth = parentEnemy.health;
            e.firstCardValue = SlimeClass.mediumSlimeFirstCard;
            e.lastCardValue = SlimeClass.mediumSlimeLastCard;
            e.health = parentEnemy.health;
            e.resistanceDamageTypes.Add(SlimeClass.physicalDmg, SlimeClass.resist);
            e.resistanceDamageTypes.Add(SlimeClass.magaicalDmg, SlimeClass.normal);
            e.resistanceDamageTypes.Add(SlimeClass.fireDmg, SlimeClass.normal);
            e.resistanceDamageTypes.Add(SlimeClass.iceDmg, SlimeClass.normal);
            e.resistanceDamageTypes.Add(SlimeClass.holyDmg, SlimeClass.normal);
            GameControllerRef.listOfEnemies.Add(e);
            e.thisEnemy.AddComponent<OnEnemyScript>().enemyRef = e;
            e.classContainsDefence = false;

            e.healthBarObject = Instantiate(Resources.Load<GameObject>("Prefabs/HP"), e.thisEnemy.transform);
            e.healthBarObject.transform.localPosition += new Vector3(0, 0.242f, 0);
            e.healthBarObject.transform.localScale = new Vector3(0.0015f, 0.0015f, 1);
            T = e.healthBarObject.transform.GetChild(0).GetComponent<TextMeshPro>();
            T.text = e.health.ToString();
            T = e.healthBarObject.transform.GetChild(1).GetComponent<TextMeshPro>();
            T.text = e.className;

            e.iconBar = e.thisEnemy.transform.GetChild(2).gameObject;
        }
        deleteEnemy = true;
        deleteEnemyID.Add(parentEnemy.EnemyID);
        parentEnemy.thisEnemy.SetActive(false);
    }

    public void SmallSlimeCreation(Enemy parentEnemy)
    {
        List<Enemy> tempE = new List<Enemy>();
        tempE.Add(new Enemy());
        tempE.Add(new Enemy());

        Vector3 newPos = new Vector3(0, -100, 0);
        Vector3 newCardPos = new Vector3(parentEnemy.thisEnemy.transform.localPosition.x - 140, 0, 0);
        foreach (Enemy e in tempE)
        {
            TextMeshPro T = new TextMeshPro();
            e.thisEnemy = Instantiate(Resources.Load<GameObject>("Prefabs/Sprites/Slime Prefab"), GameControllerRef.enemySpace.transform);
            e.thisEnemy.transform.localPosition = (parentEnemy.thisEnemy.transform.localPosition + newPos);
            newPos += new Vector3(0, 270, 0);
            e.cardPosition += newCardPos;
            newCardPos += new Vector3(210, 0, 0);
            e.thisEnemy.name = "SmallSlime";
            e.className = "SmallSlime";
            e.EnemyID = GameControllerRef.currentEnemyID;
            GameControllerRef.currentEnemyID += 1;
            e.maxHealth = parentEnemy.health;
            e.firstCardValue = SlimeClass.smallSlimeFirstCard;
            e.lastCardValue = SlimeClass.smallSlimeLastCard;
            e.health = parentEnemy.health;
            e.resistanceDamageTypes.Add(SlimeClass.physicalDmg, SlimeClass.resist);
            e.resistanceDamageTypes.Add(SlimeClass.magaicalDmg, SlimeClass.normal);
            e.resistanceDamageTypes.Add(SlimeClass.fireDmg, SlimeClass.normal);
            e.resistanceDamageTypes.Add(SlimeClass.iceDmg, SlimeClass.normal);
            e.resistanceDamageTypes.Add(SlimeClass.holyDmg, SlimeClass.normal);
            GameControllerRef.listOfEnemies.Add(e);
            e.thisEnemy.AddComponent<OnEnemyScript>().enemyRef = e;
            e.classContainsDefence = false;

            e.healthBarObject = Instantiate(Resources.Load<GameObject>("Prefabs/HP"), e.thisEnemy.transform);
            e.healthBarObject.transform.localPosition += new Vector3(0, 0.242f, 0);
            e.healthBarObject.transform.localScale = new Vector3(0.0015f, 0.0015f, 1);
            T = e.healthBarObject.transform.GetChild(0).GetComponent<TextMeshPro>();
            T.text = e.health.ToString();
            T = e.healthBarObject.transform.GetChild(1).GetComponent<TextMeshPro>();
            T.text = e.className;
            e.iconBar = e.thisEnemy.transform.GetChild(2).gameObject;
            e.thisEnemy.transform.localScale = new Vector3(250, 250, 1);
        }
        deleteEnemy = true;
        deleteEnemyID.Add(parentEnemy.EnemyID);
        parentEnemy.thisEnemy.SetActive(false);
    }
    #endregion

    #region Rat Creation
    public void RatGameScene()
    {
        RatKingCreation();
        GiantRatCreation();
        GameControllerRef.sortEnemyTransforms = true;
        GameControllerRef.sortEnemyCardTransforms = true;
    }
    public void RatKingCreation()
    {
        Enemy ratKing = new Enemy();
        ratKing.thisEnemy = Instantiate(Resources.Load<GameObject>("Prefabs/Sprites/Rat Prefab"), GameControllerRef.enemySpace.transform);
        ratKing.thisEnemy.name = "RatKing";
        ratKing.className = "RatKing";
        ratKing.child = "Rat";
        ratKing.maxHealth = 40;
        ratKing.EnemyID = 1;
        ratKing.firstCardValue = RatClass.ratKingFirstCard;
        ratKing.lastCardValue = RatClass.ratKingLastCard;
        ratKing.health = ratKing.maxHealth;
        ratKing.resistanceDamageTypes.Add(RatClass.physicalDmg, RatClass.normal);
        ratKing.resistanceDamageTypes.Add(RatClass.magaicalDmg, RatClass.normal);
        ratKing.resistanceDamageTypes.Add(RatClass.fireDmg, RatClass.weak);
        ratKing.resistanceDamageTypes.Add(RatClass.iceDmg, RatClass.normal);
        ratKing.resistanceDamageTypes.Add(RatClass.holyDmg, RatClass.normal);
        ratKing.thisEnemy.AddComponent<OnEnemyScript>().enemyRef = ratKing;
        ratKing.defenceValue = ratKing.thisEnemy.transform.GetChild(3).gameObject;
        ratKing.maxDefence = RatClass.maxDefence;
        ratKing.classContainsDefence = true;

        for (int i = ratKing.firstCardValue; i <= ratKing.lastCardValue; i++)
        {
            ratKing.deck.Add(CardsReadIn.enemyDecks[i].cardNumber);
        }

        GameControllerRef.listOfEnemies.Add(ratKing);
        ratKing.healthBarObject = Instantiate(Resources.Load<GameObject>("Prefabs/HP"), ratKing.thisEnemy.transform);
        ratKing.healthBarObject.transform.localPosition += new Vector3(0, 0.242f, 0);
        ratKing.healthBarObject.transform.localScale = new Vector3(0.0015f, 0.0015f, 1);
        TextMeshPro T = ratKing.healthBarObject.transform.GetChild(0).GetComponent<TextMeshPro>();
        T.text = ratKing.health.ToString();
        T = ratKing.healthBarObject.transform.GetChild(1).GetComponent<TextMeshPro>();
        T.text = ratKing.className;
        ratKing.iconBar = ratKing.thisEnemy.transform.GetChild(2).gameObject;
    }
    public void GiantRatCreation()
    {
        List<Enemy> temp = new List<Enemy>();
        temp.Add(new Enemy());
        temp.Add(new Enemy());
        foreach(Enemy e in temp)
        {
            e.thisEnemy = Instantiate(Resources.Load<GameObject>("Prefabs/Sprites/Rat Prefab"), GameControllerRef.enemySpace.transform);
            e.thisEnemy.name = "GaintRat";
            e.className = "GiantRat";
            e.maxHealth = 30;
            e.EnemyID = GameControllerRef.currentEnemyID;
            GameControllerRef.currentEnemyID += 1;
            e.firstCardValue = RatClass.giantRatFirstCard;
            e.lastCardValue = RatClass.giantRatLastCard;
            e.health = e.maxHealth;
            e.resistanceDamageTypes.Add(RatClass.physicalDmg, RatClass.normal);
            e.resistanceDamageTypes.Add(RatClass.magaicalDmg, RatClass.normal);
            e.resistanceDamageTypes.Add(RatClass.fireDmg, RatClass.weak);
            e.resistanceDamageTypes.Add(RatClass.iceDmg, RatClass.normal);
            e.resistanceDamageTypes.Add(RatClass.holyDmg, RatClass.normal);
            e.resistanceDamageTypes.Add(RatClass.poiDmg, RatClass.resist);
            e.thisEnemy.AddComponent<OnEnemyScript>().enemyRef = e;
            e.defenceValue = e.thisEnemy.transform.GetChild(3).gameObject;
            e.maxDefence = RatClass.maxDefence;
            e.classContainsDefence = true;

            for (int i = e.firstCardValue; i <= e.lastCardValue; i++)
            {
                e.deck.Add(CardsReadIn.enemyDecks[i].cardNumber);
            }

            GameControllerRef.listOfEnemies.Add(e);
            e.healthBarObject = Instantiate(Resources.Load<GameObject>("Prefabs/HP"), e.thisEnemy.transform);
            e.healthBarObject.transform.localPosition += new Vector3(0, 0.242f, 0);
            e.healthBarObject.transform.localScale = new Vector3(0.0015f, 0.0015f, 1);
            TextMeshPro T = e.healthBarObject.transform.GetChild(0).GetComponent<TextMeshPro>();
            T.text = e.health.ToString();
            T = e.healthBarObject.transform.GetChild(1).GetComponent<TextMeshPro>();
            T.text = e.className;
            e.iconBar = e.thisEnemy.transform.GetChild(2).gameObject;
        }
    }
    public void RatCreation(Enemy parent)
    {
        Enemy e = new Enemy();
        e.thisEnemy = Instantiate(Resources.Load<GameObject>("Prefabs/Sprites/Rat Prefab"), GameControllerRef.enemySpace.transform);
        e.thisEnemy.name = "Rat";
        e.className = "Rat";
        e.maxHealth = 5;
        e.EnemyID = GameControllerRef.currentEnemyID;
        GameControllerRef.currentEnemyID += 1;
        e.firstCardValue = RatClass.giantRatFirstCard;
        e.lastCardValue = RatClass.giantRatLastCard;
        e.health = e.maxHealth;
        e.resistanceDamageTypes.Add(RatClass.physicalDmg, RatClass.normal);
        e.resistanceDamageTypes.Add(RatClass.magaicalDmg, RatClass.normal);
        e.resistanceDamageTypes.Add(RatClass.fireDmg, RatClass.weak);
        e.resistanceDamageTypes.Add(RatClass.iceDmg, RatClass.normal);
        e.resistanceDamageTypes.Add(RatClass.holyDmg, RatClass.normal);
        e.resistanceDamageTypes.Add(RatClass.poiDmg, RatClass.resist);
        e.thisEnemy.AddComponent<OnEnemyScript>().enemyRef = e;
        e.defenceValue = e.thisEnemy.transform.GetChild(3).gameObject;
        e.maxDefence = RatClass.maxDefence;
        e.classContainsDefence = true;

        for (int i = e.firstCardValue; i <= e.lastCardValue; i++)
        {
            e.deck.Add(CardsReadIn.enemyDecks[i].cardNumber);
        }

        GameControllerRef.listOfEnemies.Add(e);

        e.healthBarObject = Instantiate(Resources.Load<GameObject>("Prefabs/HP"), e.thisEnemy.transform);
        e.healthBarObject.transform.localPosition += new Vector3(0, 0.242f, 0);
        e.healthBarObject.transform.localScale = new Vector3(0.0015f, 0.0015f, 1);
        TextMeshPro T = e.healthBarObject.transform.GetChild(0).GetComponent<TextMeshPro>();
        T.text = e.health.ToString();
        T = e.healthBarObject.transform.GetChild(1).GetComponent<TextMeshPro>();
        T.text = e.className;
        e.iconBar = e.thisEnemy.transform.GetChild(2).gameObject;
        #endregion
    }
}

