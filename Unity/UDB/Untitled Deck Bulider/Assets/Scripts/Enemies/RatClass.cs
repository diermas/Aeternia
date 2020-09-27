using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatClass : MonoBehaviour
{
    public int cardNumber;
    public static int rangeMin, rangeMax, maxDefence = 10;
    public static int ratKingFirstCard = 2101, ratKingLastCard = 2104;
    public static int giantRatFirstCard = 2105, giantRatLastCard = 2107;
    public static int ratFirstCard = 2108, ratLastCard = 2108;
    public string title, subClass, cardEffectText;
    public static string physicalDmg = "Physical", magaicalDmg = "Magical", fireDmg = "Fire", iceDmg = "Ice", holyDmg = "Holy", poiDmg = "Poison";
    public static float resist = 0.75f, normal = 1.0f, weak = 1.25f;
    public static int maxNumberOfRats = 8;

    public static void RatPositioning(int howManyEnemies, int currentEnemyinList, GameObject enemyGO)
    {
        int topRowY = 90;
        int bottomRowY = -190;
        int spacingX = 350;
        if (howManyEnemies == 8)
        {
            if (currentEnemyinList <= 3)
            {
                int x = currentEnemyinList * spacingX;
                int startingX = -800;
                enemyGO.transform.localPosition = new Vector2(startingX + x, topRowY);
            }
            else
            {
                int x = currentEnemyinList * spacingX;
                int startingX = -700;
                enemyGO.transform.localPosition = new Vector2(startingX + x, bottomRowY);
            }
        }
        else if (howManyEnemies == 7)
        {
            if (currentEnemyinList <= 3)
            {
                int x = currentEnemyinList * spacingX;
                int startingX = -800;
                enemyGO.transform.localPosition = new Vector2(startingX + x, topRowY);
            }
            else
            {
                int x = (currentEnemyinList - 4) * spacingX;
                int startingX = -550;
                enemyGO.transform.localPosition = new Vector2(startingX + x, bottomRowY);
            }
        }
        else if (howManyEnemies == 6)
        {
            if (currentEnemyinList <= 2)
            {
                int x = currentEnemyinList * spacingX;
                int startingX = -550;
                enemyGO.transform.localPosition = new Vector2(startingX + x, topRowY);
            }
            else
            {
                int x = (currentEnemyinList - 3) * spacingX;
                int startingX = -450;
                enemyGO.transform.localPosition = new Vector2(startingX + x, bottomRowY);
            }
        }
        else if (howManyEnemies == 5)
        {
            if (currentEnemyinList <= 2)
            {
                int x = currentEnemyinList * spacingX;
                int startingX = -450;
                enemyGO.transform.localPosition = new Vector2(startingX + x, topRowY);
            }
            else
            {
                int x = (currentEnemyinList - 3) * spacingX;
                int startingX = -250;
                enemyGO.transform.localPosition = new Vector2(startingX + x, bottomRowY);
            }
        }
        else if (howManyEnemies == 4)
        {
            int x = currentEnemyinList * spacingX;
            int startingX = -750;
            enemyGO.transform.localPosition = new Vector2(startingX + x, topRowY);
        }
        else if (howManyEnemies == 3)
        {
            int x = currentEnemyinList * spacingX;
            int startingX = -500;
            enemyGO.transform.localPosition = new Vector2(startingX + x, topRowY);
        }
        else if (howManyEnemies == 2)
        {
            int x = currentEnemyinList * 500;
            int startingX = -350;
            enemyGO.transform.localPosition = new Vector2(startingX + x, topRowY);
        }
        else if (howManyEnemies == 1)
        {
            int startingX = 0;
            enemyGO.transform.localPosition = new Vector2(startingX, topRowY);
        }
    }

    public static void CardPositioning(int howManyCards, int currentCardInList, GameObject cardGO)
    {
        int y = -450;
        int spacingX = 210 * currentCardInList;
        if (howManyCards == 8)
        {
            int xSpacing = 195 * currentCardInList;
            int startingX = -900;
            int yVal = -520;
            cardGO.transform.localPosition = new Vector2(startingX + xSpacing, yVal);
        }
        else if (howManyCards == 7)
        {
            int startingX = -850;
            cardGO.transform.localPosition = new Vector2(startingX + spacingX, y);
        }
        else if (howManyCards == 6)
        {
            int startingX = -730;
            cardGO.transform.localPosition = new Vector2(startingX + spacingX, y);
        }
        else if (howManyCards == 5)
        {
            int startingX = -575;
            cardGO.transform.localPosition = new Vector2(startingX + spacingX, y);
        }
        else if (howManyCards == 4)
        {
            int startingX = -450;
            cardGO.transform.localPosition = new Vector2(startingX + spacingX, y);
        }
        else if (howManyCards == 3)
        {
            int startingX = -320;
            cardGO.transform.localPosition = new Vector2(startingX + spacingX, y);
        }
        else if (howManyCards == 2)
        {
            int startingX = -200;
            cardGO.transform.localPosition = new Vector2(startingX + spacingX, y);
        }
        else if (howManyCards == 1)
        {
            int startingX = -100;
            cardGO.transform.localPosition = new Vector2(startingX + spacingX, y);
        }
    }
}
