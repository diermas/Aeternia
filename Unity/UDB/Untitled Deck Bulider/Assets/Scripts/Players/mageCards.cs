using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Mage Cards", menuName = "Cards/Mage Cards")] // creates a new folder to store the cards being created //
public class mageCards : cardClass
{
    public Sprite image;

    public static int firstCard = 1, lastCard = 33;
    public static int starterCardFV = 1, starterCardLV = 3;

    public static string resourceName = "Mana";

    public static int turnOrderValue = 40;
    public static int startingHealth = 20;

    void Start()
    {
        
    }


    void Update()
    {
        
    }
}
