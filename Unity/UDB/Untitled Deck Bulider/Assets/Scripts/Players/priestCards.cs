using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Priest Cards", menuName = "Cards/Priest Cards")] // creates a new folder to store the cards being created //
public class priestCards : cardClass
{
    public Sprite image;

    public static int firstCard = 600, lastCard = 632;
    public static int starterCardFV = 600, starterCardLV = 602;

    public static string resourceName = "Faith";


    public static int turnOrderValue = 60;
    public static int startingHealth = 20;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
