using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Warrior Cards", menuName = "Cards/Warrior Cards")] // creates a new folder to store the cards being created //
public class warriorCards : cardClass
{
    public Sprite image;
    
    public static string resourceName = "Bloodlust";

    public static int firstCard = 400, lastCard = 432;
    public static int starterCardFV = 400, starterCardLV = 402;

    public static int turnOrderValue = 80;
    public static int startingHealth = 30;
    void Start()
    {
    }


    void Update()
    {
        
    }

}
