using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quests : MonoBehaviour
{
    private string QuestName, QuestDescription, objective, rewardType;
    private string[] Location;
    private bool ObjectiveMet;
    private int rewards;

    Quests(string Name, string Descrition, int NumofLocations, string reward, int id)
    {
        QuestName = Name;
        QuestDescription = Descrition;
        Location = new string[NumofLocations];
        rewardType = reward;
        rewards = id;
    }
    Quests(string Name, string Descrition, string reward, int id)
    {
        QuestName = Name;
        QuestDescription = Descrition;
        rewardType = reward;
        rewards = id;
    }

    public void setLocation(string[] data)
    {
        for(int i = 0; i < Location.Length;i++)
        {
            Location[i] = data[i];
        }
    }

    public void GiveRewoard(UiAndColection players)
    {
        if(rewardType.CompareTo("Gold")==0)
        {
            players.PlayerGold.AddGold(rewards);
        }
        else
        {
            players.addCard(rewards);
        }

    }

    public void setObjective()
    {

    }
}
