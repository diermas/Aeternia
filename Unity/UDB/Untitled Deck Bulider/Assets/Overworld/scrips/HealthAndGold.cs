using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class HealthAndGold : MonoBehaviour
{
    public TextMeshProUGUI Warior, Rough, Preast, Mage, Gold;
    private int goldcount = 0;


    void Start()
    {
        setHealthM(PlayerCreation.playerMage.health);
        setHealthW(PlayerCreation.playerWarrior.health);
        setHealthP(PlayerCreation.playerPriest.health);
        setHealthR(PlayerCreation.playerRogue.health);
    }
    public void setGold()
    {
        Gold.SetText("Gold: " + goldcount);
    }

    public void AddGold(int amount)
    {
        goldcount += amount;
    }

    public bool SubtractGold(int amount)
    {
        if(goldcount.CompareTo(amount) >= 0)
        {
            goldcount -= amount;
            return true;
        }
        return false;
    }

    public void setHealthW(int Health)
    {
        Warior.SetText("Warrior Health: " + Health);
    }

    public void setHealthP(int Health)
    {
        Preast.SetText("Priest Health: " + Health);
    }

    public void setHealthM(int Health)
    {
        Mage.SetText("Mage Health: " + Health);
    }

    public void setHealthR(int Health)
    {
        Rough.SetText("Rogue Health: " + Health);
    }
}
