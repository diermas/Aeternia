using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Rogue Cards", menuName = "Cards/Rogue Cards")] // creates a new folder to store the cards being created //
public class rogueCards : cardClass
{
    public Sprite image;

    public static int firstCard = 200, lastCard = 232;
    public static int starterCardFV = 200, starterCardLV = 202;

    public static string resourceName = "Focus";

    public static int turnOrderValue = 20;
    public static int startingHealth = 20;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
