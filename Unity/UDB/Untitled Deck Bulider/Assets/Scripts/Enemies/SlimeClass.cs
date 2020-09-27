using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeClass : MonoBehaviour
{
    public int cardNumber;
    public int rangeMin, rangeMax;
    public static int largeSlimeFirstCard = 2001, largeSlimeLastCard = 2004;
    public static int mediumSlimeFirstCard = 2005, mediumSlimeLastCard = 2008;
    public static int smallSlimeFirstCard = 2009, smallSlimeLastCard = 2011;
    public string title, subClass, cardEffectText;
    public static int largeSlimeHealth = 30;
    public static string physicalDmg = "Physical", magaicalDmg = "Magical", fireDmg = "Fire", iceDmg = "Ice", holyDmg = "Holy", poiDmg = "Poison";
    public static float resist = 0.75f, normal = 1.0f;
}
